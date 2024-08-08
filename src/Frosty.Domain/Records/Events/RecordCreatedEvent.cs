
using Frosty.Domain.Framework;

namespace Frosty.Domain.Records.Events;

public sealed record RecordCreatedEvent(Guid RecordId) : IDomainEvent;
