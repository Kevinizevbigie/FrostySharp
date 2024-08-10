
using Frosty.Domain.Framework;

namespace Frosty.Domain.Records.Events;

public sealed record AddToEmailPipelineDomainEvent(
    Guid RecordId) : IDomainEvent;
