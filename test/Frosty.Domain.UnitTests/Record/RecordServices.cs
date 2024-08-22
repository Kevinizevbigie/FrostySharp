using Frosty.Domain.Framework;
using Frosty.Domain.Records;
using Frosty.Domain.Records.Services;

namespace Frosty.Domain.UnitTests.Records;

internal class WebsitePingTrue : IPingWebsiteService {
    public async Task<bool> Ping(string website) {
        return true;
    }
}

internal class WebsitePingFalse : IPingWebsiteService {
    public async Task<bool> Ping(string website) {
        return false;
    }
}

internal class DuplicateCheckServiceFailed : IRecordCheckDuplicateService {
    public async Task<bool> Check(Website website) {
        return false;
    }
}

internal class DuplicateCheckServiceSuccess : IRecordCheckDuplicateService {
    public async Task<bool> Check(Website website) {
        return true;
    }
}

internal class EmailVerifyServicePass : IEmailVerificationService {
    public async
        Task<Result<List<EmailVerificationResponse>>> Send(
            Guid id, List<EmailGuess> list) {
        var l =
            new EmailVerificationResponse(
                "udjeudir",
                new EmailGuess("nice@gmail.com")
                , EmailGuessStatus.Passed);

        var alist = new List<EmailVerificationResponse>();

        alist.Add(l);

        return Result.Success<List<EmailVerificationResponse>>(alist);
    }
}


internal static class RecordServices {
    internal static WebsitePingTrue PingTrue = new();
    internal static WebsitePingFalse PingFalse = new();

    internal static DuplicateCheckServiceFailed DupCheckFail = new();
    internal static DuplicateCheckServiceSuccess DupCheckSucceed = new();

    internal static EmailVerifyServicePass VerifyPass = new();
}
