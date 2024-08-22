using Frosty.Domain.Framework;

namespace Frosty.Domain.Records.Events;

public sealed record RejectRecordDomainEvent(Guid RecordId) : IDomainEvent;

