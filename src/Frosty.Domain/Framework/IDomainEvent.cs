using MediatR;

namespace Frosty.Domain.Framework;

// a domain event uses MediatR INotification
interface IDomainEvent : INotification {
}
