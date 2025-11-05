// CoreBanking.Core/Common/IDomainEvent.cs
namespace CoreBanking.Core.Common;

public interface IDomainEvent

{

    Guid EventId { get; }

    DateTime OcurredOn { get; }

    string EventType { get; }

}
