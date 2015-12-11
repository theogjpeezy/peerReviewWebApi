using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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
		private readonly IAttaboyAndGoofRepository _attaboyAndGoofRepo = new AttaboyAndGoofRepository();

		public HttpResponseMessage PostAttaboy(Attaboy newAttaboy) {
			Attaboy createdAttaboy = _attaboyAndGoofRepo.CreateAttaboy(newAttaboy);
			if (createdAttaboy == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			var response = Request.CreateResponse<Attaboy>(HttpStatusCode.Created, createdAttaboy);

			return response;
		}

		public IEnumerable<Attaboy> GetAttaboy(int userId) {
			return _attaboyAndGoofRepo.GetAllAttaboys(userId);
		}

    }
}
