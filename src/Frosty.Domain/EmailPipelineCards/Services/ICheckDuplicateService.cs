namespace Frosty.Domain.EmailPipelineCards.Services;

public interface ICheckDuplicateService {
    public bool Search(Guid recordid);
}
