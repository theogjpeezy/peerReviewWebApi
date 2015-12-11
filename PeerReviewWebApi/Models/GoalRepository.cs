using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PeerReviewWebApi.Models {
	public class GoalRepository : IGoalRepository {

		private const string CREATE_GOAL_SPROC = "createGoal";
	    private const string GET_GOAL_INFO_SPROC = "getGoalInfo";

		public Goal CreateGoal(Goal newGoal) {

			DatabaseProviderFactory dbFactory = new DatabaseProviderFactory();
		    Database peerReviewDb = dbFactory.Create("PeerReviewDatabase");

			int newGoalId;
			using (DbCommand createGoalSproc = peerReviewDb.GetStoredProcCommand(CREATE_GOAL_SPROC)) {
				createGoalSproc.CommandType = CommandType.StoredProcedure;
				peerReviewDb.AddInParameter(createGoalSproc,"userId",DbType.Int16,newGoal.UserId);
				peerReviewDb.AddInParameter(createGoalSproc,"title",DbType.String, newGoal.Title);
				peerReviewDb.AddInParameter(createGoalSproc, "beginDate", DbType.DateTime, newGoal.BeginDateTime);
				peerReviewDb.AddInParameter(createGoalSproc, "endDate", DbType.DateTime, newGoal.EndDateTime);
				peerReviewDb.AddInParameter(createGoalSproc, "details", DbType.String, newGoal.Details);
				peerReviewDb.AddInParameter(createGoalSproc, "isActive", DbType.Boolean, newGoal.IsActive);
				peerReviewDb.AddInParameter(createGoalSproc, "userGoalNumber", DbType.Int16, null); // null for now, incorporate this later

				newGoalId = (int)peerReviewDb.ExecuteScalar(createGoalSproc);
			}

			Goal createdGoal = newGoal;
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
			throw new NotImplementedException();
		}

		public void DeleteGoal(int id) {
			throw new NotImplementedException();
		}
	}
}