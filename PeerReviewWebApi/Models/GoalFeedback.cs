using System;

namespace PeerReviewWebApi.Models {
    /// <summary>
    /// Model that represents the feedback elements for a users goal.
    /// </summary>
    public class GoalFeedback : Feedback {
    public int GoalId { get; set; }
    }

}