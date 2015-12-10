using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PeerReviewWebApi.Models;

namespace PeerReviewWebApi.Controllers
{
	public class UserController : ApiController
    {
		static readonly IUserRepository UserRepo = new UserRepository();

	    public User GetUser(int id) {

			User resultUser = UserRepo.Get(id);
				if (resultUser == null) {
					throw new HttpResponseException(HttpStatusCode.NotFound);
				}

		 return resultUser;

	    }
    }

	

}
