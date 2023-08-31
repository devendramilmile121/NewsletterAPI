namespace Application.Subscribers.Queries.GetSubscribers
{
    public class SubscribersVM
    {
        public IList<ReferralSourceDTO> Sources { get; set; } = new List<ReferralSourceDTO>();
        public IList<SubscriberDTO> Subscribers { get; set; } = new List<SubscriberDTO>();
    }
}
