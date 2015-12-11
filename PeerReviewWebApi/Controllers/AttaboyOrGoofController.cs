using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using PeerReviewWebApi.Models;

namespace PeerReviewWebApi.Controllers {
	[EnableCors("*", "*", "*")]
	public class AttaboyOrGoofController : ApiController{
		
		public HttpResponseMessage PostGoal(Goal newGoal) {
			AttaboyAndGoof attaboyOrGoof = GoalRepo.CreateGoal(newGoal);
			if (createdGoal == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			
			var response = Request.CreateResponse<Goal>(HttpStatusCode.Created, createdGoal);
			
			return response;
		}

		public IEnumerable<Goal> GetGoal(int userId) {
			IEnumerable<Goal> goalListForUser = GoalRepo.GetGoalsByUserId(userId);
			if (!goalListForUser.Any()) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			return goalListForUser;
		}

	    public HttpResponseMessage Get(int id) {
	        return Request.CreateResponse(HttpStatusCode.OK, GoalRepo.GetGoal(id));
	    }
    }
	}
}