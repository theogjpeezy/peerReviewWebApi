using System.Collections.Generic;

namespace PeerReviewWebApi.Models {
	public class GoalWithFeedback {
		public Goal Goal;
		public IEnumerable<GoalFeedback> Feedback;
	}
}