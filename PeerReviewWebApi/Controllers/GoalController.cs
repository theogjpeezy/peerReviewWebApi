using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using PeerReviewWebApi.Models;

namespace PeerReviewWebApi.Controllers
{
	[EnableCors("*", "*", "*")]
    public class GoalController : ApiController
    {
		static readonly IGoalRepository GoalRepo = new GoalRepository();

		public GoalController() {
			int i = 1;
		}

		[HttpPost]
		public HttpResponseMessage PostGoal(Goal newGoal) {
			Goal createdGoal = GoalRepo.CreateGoal(newGoal);
			if (createdGoal == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			
			var response = Request.CreateResponse<Goal>(HttpStatusCode.Created, createdGoal);
			
			return response;
		}
    }
}
