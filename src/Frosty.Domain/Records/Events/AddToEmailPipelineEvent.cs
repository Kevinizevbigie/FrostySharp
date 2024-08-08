
using Frosty.Domain.Framework;

namespace Frosty.Domain.Records.Events;

public sealed record AddToEmailPipelineEvent(Guid RecordId) : IDomainEvent;
