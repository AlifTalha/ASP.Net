using System;

namespace MeetingMinutesManagement.Models
{
    public class MeetingMinutesMaster
    {
        public int MeetingMinutesMasterId { get; set; }
        public string CustomerType { get; set; } // "Corporate" or "Individual"
        public int CustomerId { get; set; }
        public DateTime MeetingDate { get; set; }
        public TimeSpan MeetingTime { get; set; }
        public string MeetingPlace { get; set; }
        public string AttendsFromClient { get; set; }
        public string AttendsFromHost { get; set; }
        public string MeetingAgenda { get; set; }
        public string MeetingDiscussion { get; set; }
        public string MeetingDecision { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}