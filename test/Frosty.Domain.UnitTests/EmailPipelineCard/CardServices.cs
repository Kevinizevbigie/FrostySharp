
using Frosty.Domain.EmailPipelineCards;
using Frosty.Domain.EmailPipelineCards.Services;
using Frosty.Domain.Framework;

namespace Frosty.Domain.UnitTests.EmailPipelineCards;

internal class SendingQueueTrue : IAddToSendQueueService {
    public async Task<bool> Add(string firstname, string email) {
        return true;
    }
}

public static class CardServices {
    internal static readonly SendingQueueTrue SendingTrue = new();
}
