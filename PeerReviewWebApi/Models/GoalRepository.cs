using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PeerReviewWebApi.Models {
	public class GoalRepository : IGoalRepository {

		private const string CREATE_GOAL_SPROC = "createGoal";
		private const string UPDATE_GOAL_SPROC = "updateGoalInfo";
		private const string DEACTIVATE_GOAL_SPROC = "deactivateGoal";
	    private const string GET_GOAL_INFO_SPROC = "getGoalInfo";
		private const string GET_ALL_USER_GOALS_SPROC = "getAllGoalsForUser";

		private static readonly DatabaseProviderFactory dbFactory = new DatabaseProviderFactory();
		private readonly Database _peerReviewDb = dbFactory.Create("PeerReviewDatabase");


		public Goal CreateGoal(Goal newGoal) {

			int newGoalId;
			using (DbCommand createGoalSproc = _peerReviewDb.GetStoredProcCommand(CREATE_GOAL_SPROC)) {
				createGoalSproc.CommandType = CommandType.StoredProcedure;
				_peerReviewDb.AddInParameter(createGoalSproc,"userId",DbType.Int16,newGoal.UserId);
				_peerReviewDb.AddInParameter(createGoalSproc,"title",DbType.String, newGoal.Title);
				_peerReviewDb.AddInParameter(createGoalSproc, "beginDate", DbType.DateTime, newGoal.BeginDateTime);
				_peerReviewDb.AddInParameter(createGoalSproc, "endDate", DbType.DateTime, newGoal.EndDateTime);
				_peerReviewDb.AddInParameter(createGoalSproc, "details", DbType.String, newGoal.Details);
				_peerReviewDb.AddInParameter(createGoalSproc, "isActive", DbType.Boolean, newGoal.IsActive);
				_peerReviewDb.AddInParameter(createGoalSproc, "userGoalNumber", DbType.Int16, null); // null for now, incorporate this later

				newGoalId = (int)_peerReviewDb.ExecuteScalar(createGoalSproc);
			}

			Goal createdGoal = GetGoal(newGoalId);
			createdGoal.Id = newGoalId;

			return createdGoal;
		}

		public Goal GetGoal(int id) {
            DatabaseProviderFactory dbFactory = new DatabaseProviderFactory();
            Database peerReviewDb = dbFactory.Create("PeerReviewDatabase");
            Goal goal = new Goal();
            using (DbCommand getGoalInfo = peerReviewDb.GetStoredProcCommand(GET_GOAL_INFO_SPROC)) {
                getGoalInfo.CommandType = CommandType.StoredProcedure;
                peerReviewDb.AddInParameter(getGoalInfo, "goalId", DbType.Int32, id);
                using (IDataReader reader = peerReviewDb.ExecuteReader(getGoalInfo)) {
                    while (reader.Read()) {
                        goal.BeginDateTime = DateTime.Parse(reader["beginDate"].ToString());
                        goal.EndDateTime = DateTime.Parse(reader["endDate"].ToString());
                        goal.Id = int.Parse(reader["goalId"].ToString());
                        goal.UserId = int.Parse(reader["userId"].ToString());
                        goal.Title = reader["title"].ToString();
                        goal.Details = reader["details"].ToString();
                        goal.IsActive = bool.Parse(reader["isActive"].ToString());
                    }
                }
            }
		    return goal;
		}

		public Goal UpdateGoal(Goal goalToUpdate) {
			using (DbCommand updateGoalSproc = _peerReviewDb.GetStoredProcCommand(UPDATE_GOAL_SPROC)) {
				updateGoalSproc.CommandType = CommandType.StoredProcedure;
				_peerReviewDb.AddInParameter(updateGoalSproc, "goalId", DbType.Int16, goalToUpdate.Id);
				_peerReviewDb.AddInParameter(updateGoalSproc, "title", DbType.String, goalToUpdate.Title);
				_peerReviewDb.AddInParameter(updateGoalSproc, "beginDate", DbType.DateTime, goalToUpdate.BeginDateTime);
				_peerReviewDb.AddInParameter(updateGoalSproc, "endDate", DbType.DateTime, goalToUpdate.EndDateTime);
				_peerReviewDb.AddInParameter(updateGoalSproc, "details", DbType.String, goalToUpdate.Details);
				_peerReviewDb.AddInParameter(updateGoalSproc, "userGoalNumber", DbType.Int16, null);
				_peerReviewDb.ExecuteScalar(updateGoalSproc);
			}

			if (!goalToUpdate.IsActive) {
				using (DbCommand deactivateGoalSproc = _peerReviewDb.GetStoredProcCommand(DEACTIVATE_GOAL_SPROC)) {
					deactivateGoalSproc.CommandType = CommandType.StoredProcedure;
					_peerReviewDb.AddInParameter(deactivateGoalSproc, "goalId", DbType.Int16, goalToUpdate.Id);
					_peerReviewDb.ExecuteScalar(deactivateGoalSproc);
				}	
			}
			Goal updatedGoal = GetGoal(goalToUpdate.Id);
			return updatedGoal;
		}

		public void DeleteGoal(int id) {
			throw new NotImplementedException();
		}

		public IEnumerable<Goal> GetGoalsByUserId(int userId) {
			Collection<Goal> goals = new Collection<Goal>();
			using (
				DbCommand getGoalsForUserSproc = _peerReviewDb.GetSqlStringCommand(GET_ALL_USER_GOALS_SPROC)) {
				getGoalsForUserSproc.CommandType = CommandType.StoredProcedure;
				_peerReviewDb.AddInParameter(getGoalsForUserSproc, "userId", DbType.Int16, userId);
				using (IDataReader sprocReader = _peerReviewDb.ExecuteReader(getGoalsForUserSproc)) {
					while (sprocReader.Read()) {
						goals.Add(new Goal {
							Id = (int)sprocReader["goalId"],
							BeginDateTime = (DateTime)sprocReader["beginDate"],
							Details = sprocReader["details"].ToString(),
							EndDateTime = (DateTime)sprocReader["endDate"],
							IsActive = (bool)sprocReader["isActive"],
							Title = sprocReader["title"].ToString()
						});
					}
				}
			}
			return goals;
		}
	}
}