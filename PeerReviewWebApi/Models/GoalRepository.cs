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
			throw new NotImplementedException();
		}

		public Goal UpdateGoal(Goal goalToUpdate) {
			throw new NotImplementedException();
		}

		public void DeleteGoal(int id) {
			throw new NotImplementedException();
		}
	}
}