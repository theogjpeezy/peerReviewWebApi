using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeerReviewWebApi.Models {
	public class GoalFeedback {
		public User User { get; set; }
		public Goal goal { get; set; }
		public DateTime DateTimeSubmitted { get; set; }
		public int Rating { get; set; }
		public string Comments { get; set; }
		public User Reviewer { get; set; }
	}
}