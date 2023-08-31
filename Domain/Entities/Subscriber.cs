using Domain.Common;

namespace Domain.Entities
{
    public class Subscriber: BaseAuditableEntity
    {
        public string Email { get; set; }
        public string? Reason{ get; set; }
        public string? Other{ get; set; }
        public int ReferralSourceId { get; set; }
        public ReferralSource ReferralSource { get; set; }
    }
}
