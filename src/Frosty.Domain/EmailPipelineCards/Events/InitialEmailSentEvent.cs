
using Frosty.Domain.Framework;

namespace Frosty.Domain.EmailPipelineCards.Events;

public sealed record InitialEmailSentEvent(Guid RecordId) : IDomainEvent;
