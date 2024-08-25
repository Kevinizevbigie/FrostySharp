
namespace Frosty.Domain.EmailPipelineCards.Services;

public interface IAddToSendQueueService {
    public bool Add(
        string firstname,
        string email
    );
}
