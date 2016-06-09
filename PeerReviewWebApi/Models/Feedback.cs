using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeerReviewWebApi.Models{

    /// <summary>
    /// Generic Feedback model that can be used for anything
    /// </summary>
    public class Feedback
    {
        public int Id { get; set; }
        public DateTime DateTimeSubmitted { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
    }
}