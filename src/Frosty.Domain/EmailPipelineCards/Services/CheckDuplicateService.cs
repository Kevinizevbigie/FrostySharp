
namespace Frosty.Domain.EmailPipelineCards.Services;

public interface CheckDuplicateService {
    public bool Search(Guid recordid);
}
