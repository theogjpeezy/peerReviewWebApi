using System.Collections.Generic;

namespace PeerReviewWebApi.Models {
	interface IGoalRepository {
		Goal CreateGoal(Goal newGoal);
		Goal GetGoal(int id);
		Goal UpdateGoal(Goal goalToUpdate);
		void DeleteGoal(int id);
		IEnumerable<Goal> GetGoalsByUserId(int userId);
	}
}
