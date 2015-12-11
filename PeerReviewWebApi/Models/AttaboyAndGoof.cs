using System;

namespace PeerReviewWebApi.Models {
	/// <summary>
	/// A Model that represents a type of positive or negative feedback that a User can give for something not goal related.
	/// </summary>
	public abstract class AttaboyAndGoof {
		public int Id { get; set; }
		public int UserId { get; set; }
		public DateTime DateTimeSubmitted { get; set; }
		public string Comment { get; set; }
		public int? SubmitterId { get; set; }
		public bool IsAnonymous { get; set; }
	}
}