
using Frosty.Domain.Framework;
using Frosty.Domain.Records.Services;

namespace Frosty.Domain.Records;

public sealed record Website {

    public string Value;


    private Website(
        string website
    ) {
        Value = website;
    }

    async public static Task<Result<Website>> Create(
        string website,
        IPingWebsiteService<Website> service
    ) {

        //TODO: Add if website is blank 

        var res = await service.Ping(website);

        if (res.IsSuccess == false) {
            return Result.Failure<Website>(RecordErrors.WebsiteRejected);
        }

        var ws = new Website(website);

        return Result.Success<Website>(ws);

    }

}


