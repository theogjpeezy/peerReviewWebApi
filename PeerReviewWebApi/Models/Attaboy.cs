using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeerReviewWebApi.Models {
	/// <summary>
	/// A Model that represents a type of feedback that a User can give for something not goal related.
	/// </summary>
	public class Attaboy {
		public User User { get; set; }
		public DateTime DateTimeSubmitted { get; set; }
		public int Rating { get; set; }
		public string Comments { get; set; }
		public User Reviewer { get; set; }
		public bool IsAnonymous { get; set; }
	}
}