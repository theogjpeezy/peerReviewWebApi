using System.Collections.Generic;

namespace PeerReviewWebApi.Models {
	interface IGoalFeedbackRepository {
		GoalFeedback CreateGoalFeedback(GoalFeedback newGoalFeedback);
		IEnumerable<GoalWithFeedback> GetGoalsWithFeedback(int userId);
	}
}
