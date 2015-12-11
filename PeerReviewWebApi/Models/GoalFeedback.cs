using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeerReviewWebApi.Models {
	/// <summary>
	/// Model that represents the feedback elements for a users goal.
	/// </summary>
	public class GoalFeedback {
		public int Id { get; set; }
		public int UserId { get; set; }
		public int GoalId { get; set; }
		public DateTime DateTimeSubmitted { get; set; }
		public int Rating { get; set; }
		public string Comments { get; set; }
		public int ReviewerId { get; set; }
	}
}