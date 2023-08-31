namespace Application.Subscribers.Queries.GetSubscribers
{
    public class SubscriberDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? Reason { get; set; }
        public string? Other { get; set; }
        public int ReferralSourceId { get; set; }
        public ReferralSourceDTO ReferralSource { get; set; }
    }
}
