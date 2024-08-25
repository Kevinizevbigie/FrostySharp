
namespace Frosty.Domain.EmailPipelineCards.Services;

public interface IAddToSendQueueService {
    public Task<bool> Add(
        string firstname,
        string email
    );
}
