using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace PeerReviewWebApi.Models {
	public class User {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public User Manager { get; set; }
		public IEnumerable<User> TeamMembers { get; set; }
		public IEnumerable<Goal> Goals { get; set; }
	}
}