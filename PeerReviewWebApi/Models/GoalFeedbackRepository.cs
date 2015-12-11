using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PeerReviewWebApi.Models {
	public class GoalFeedbackRepository : IGoalFeedbackRepository {

		private const string CREATE_FEEDBACK_SPROC = "createGoalFeedback";
		private const string GET_FEEDBACK_SPROC = "getGoalFeedback";

		private static readonly DatabaseProviderFactory dbFactory = new DatabaseProviderFactory();
		private readonly Database _peerReviewDb = dbFactory.Create("PeerReviewDatabase");

		public GoalFeedback CreateGoalFeedback(GoalFeedback newGoalFeedback) {
			int newGoalFeedbackId;
			using (DbCommand createGoalFeedbackSproc = _peerReviewDb.GetStoredProcCommand(CREATE_FEEDBACK_SPROC)) {
				createGoalFeedbackSproc.CommandType = CommandType.StoredProcedure;
				_peerReviewDb.AddInParameter(createGoalFeedbackSproc, "reviewerId", DbType.Int16, newGoalFeedback.ReviewerId);
				_peerReviewDb.AddInParameter(createGoalFeedbackSproc, "goalId", DbType.Int16, newGoalFeedback.GoalId);
				_peerReviewDb.AddInParameter(createGoalFeedbackSproc, "submitted", DbType.DateTime, newGoalFeedback.DateTimeSubmitted);
				_peerReviewDb.AddInParameter(createGoalFeedbackSproc, "rating", DbType.Int16, newGoalFeedback.Rating);
				_peerReviewDb.AddInParameter(createGoalFeedbackSproc, "comments", DbType.String, newGoalFeedback.Comments);
				newGoalFeedbackId = int.Parse(_peerReviewDb.ExecuteScalar(createGoalFeedbackSproc).ToString());
			}

			//GoalFeedback createdGoalFeedback = GetGoalFeedback(newGoalFeedbackId);
			GoalFeedback createdGoalFeedback = newGoalFeedback;
			createdGoalFeedback.Id = newGoalFeedbackId;

			return createdGoalFeedback;
		}
	}
}