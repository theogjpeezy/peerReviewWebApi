using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerReviewWebApi.Models {
	interface IGoalRepository {
		//Create goal - return goal
		Goal CreateGoal(Goal newGoal);

		// Get goal by id, return goal
		Goal GetGoal(int id);

		//update goal by goal, return goal
		Goal UpdateGoal(Goal goalToUpdate);

		//delete goal by id, return nothing
		void DeleteGoal(int id);
	}
}
