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
    public class GoalFeedbackController : ApiController {

		static readonly IGoalFeedbackRepository GoalFeedbackRepo = new GoalFeedbackRepository();

		public HttpResponseMessage PostGoalFeedback(GoalFeedback newGoalFeedback) {
			GoalFeedback createdGoalFeedback = GoalFeedbackRepo.CreateGoalFeedback(newGoalFeedback);
			if (createdGoalFeedback == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			var response = Request.CreateResponse<GoalFeedback>(HttpStatusCode.Created, createdGoalFeedback);

			return response;
		}


    }
}
