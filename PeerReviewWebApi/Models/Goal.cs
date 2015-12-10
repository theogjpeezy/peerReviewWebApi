using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeerReviewWebApi.Models {

	/// <summary>
	/// A model that represents a Goal that someone has made. 
	/// </summary>
	public class Goal {
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime BeginDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public string Details { get; set; }
		public bool IsActive { get; set; }

	}
}