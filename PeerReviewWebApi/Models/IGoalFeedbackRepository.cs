using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerReviewWebApi.Models {
	interface IGoalFeedbackRepository {
		GoalFeedback CreateGoalFeedback(GoalFeedback newGoalFeedback);
	}
}
