using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeerReviewWebApi.Models {
	public class Attaboy {
		public User User { get; set; }
		public DateTime DateTimeSubmitted { get; set; }
		public int Rating { get; set; }
		public string Comments { get; set; }
		public User Reviewer { get; set; }
		public bool IsAnonymous { get; set; }
	}
}