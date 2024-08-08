
using Frosty.Domain.Framework;

namespace Frosty.Domain.Record.Events;

public sealed record RecordCreatedEvent(Guid RecordId) : IDomainEvent;
