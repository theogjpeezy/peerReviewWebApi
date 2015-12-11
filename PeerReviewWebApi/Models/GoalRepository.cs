﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PeerReviewWebApi.Models {
	public class GoalRepository : IGoalRepository {

		private const string CREATE_GOAL_SPROC = "createGoal";
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

			Goal createdGoal = newGoal;
			createdGoal.Id = newGoalId;

			return createdGoal;
		}

		public Goal GetGoal(int id) {
			throw new NotImplementedException();
		}

		public Goal UpdateGoal(Goal goalToUpdate) {
			throw new NotImplementedException();
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