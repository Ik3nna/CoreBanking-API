using MediatR;

namespace CoreBanking.Core.Common
{
    public abstract record DomainEvent : IDomainEvent, INotification
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OcurredOn { get; } = DateTime.UtcNow;
        public string EventType => GetType().Name;
    }
}
