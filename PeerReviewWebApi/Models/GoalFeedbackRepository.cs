using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PeerReviewWebApi.Models {
	public class GoalFeedbackRepository : IGoalFeedbackRepository {

		private const string CREATE_FEEDBACK_SPROC = "createGoalFeedback";
		private const string GET_FEEDBACK_SPROC = "getGoalFeedback";
		private const string GET_ALL_GOAL_FEEDBACK_SPROC = "getAllGoalFeedback";

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

		public GoalWithFeedback GetGoalWithFeedback(int goalId) {
			Collection<GoalFeedback> goalFeedbacks = new Collection<GoalFeedback>();

			// Get the goal
			GoalRepository GoalRepo = new GoalRepository();
			Goal goal = GoalRepo.GetGoal(goalId);

			// Get the feedback related to the goal
			using (DbCommand createGoalFeedbackSproc = _peerReviewDb.GetStoredProcCommand(GET_ALL_GOAL_FEEDBACK_SPROC)) {
				createGoalFeedbackSproc.CommandType = CommandType.StoredProcedure;
				_peerReviewDb.AddInParameter(createGoalFeedbackSproc, "goalId", DbType.Int16, goal.Id);

				using (IDataReader sprocReader = _peerReviewDb.ExecuteReader(createGoalFeedbackSproc)) {
					while (sprocReader.Read()) {
						goalFeedbacks.Add(new GoalFeedback() {
							Comments = sprocReader["comments"].ToString(),
							DateTimeSubmitted = DateTime.Parse(sprocReader["submitted"].ToString()),
							GoalId = goal.Id,
							Id = int.Parse(sprocReader["feedbackId"].ToString()),
							Rating = int.Parse(sprocReader["rating"].ToString()),
							ReviewerId = int.Parse(sprocReader["reviewerId"].ToString())
						});
					}
				}	
			}
			return new GoalWithFeedback() { Feedback = goalFeedbacks, Goal = goal };
		}

		public IEnumerable<GoalWithFeedback> GetGoalsWithFeedback(int userId) {
			Collection<GoalWithFeedback> goalsWithFeedback = new Collection<GoalWithFeedback>();

			GoalRepository GoalRepo = new GoalRepository();
			IEnumerable<Goal> goals = GoalRepo.GetGoalsByUserId(userId);
			foreach (Goal goal in goals) {
				goalsWithFeedback.Add(GetGoalWithFeedback(goal.Id));
			}

			return goalsWithFeedback;
		} 


	}
}