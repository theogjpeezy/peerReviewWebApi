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
    public class AttaboyController : ApiController
    {
		static readonly IAttaboyAndGoofRepository GoalFeedbackRepo = new AttaboyAndGoofRepository();


    }
}
