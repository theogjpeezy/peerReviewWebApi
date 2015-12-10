using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerReviewWebApi.Models {

	/// <summary>
	/// Interface defining the required functions for working with Users
	/// </summary>
	interface IUserRepository {
		
		/// <summary>
		/// Get a user by their user id
		/// </summary>
		/// <param name="id">User id to retrieve</param>
		/// <returns>The user associated with the given id</returns>
		User Get(int id);
	}
}
