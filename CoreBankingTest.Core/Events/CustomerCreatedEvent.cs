using CoreBanking.Core.Common;
using CoreBanking.Core.ValueObjects;

namespace CoreBanking.Core.Events
{
    public record CustomerCreatedEvent : DomainEvent
    {
        public CustomerId CustomerId { get; }

        public CustomerCreatedEvent(CustomerId customerId)
        {
            CustomerId = customerId;
        }
    }
}
