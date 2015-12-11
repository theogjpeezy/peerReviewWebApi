using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

using PeerReviewWebApi.Models;

namespace PeerReviewWebApi.Controllers
{
    [EnableCors("*", "*", "*")]
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
