using Frosty.Domain.Framework;
using Frosty.Domain.Records;
using Frosty.Domain.Shared;

namespace Frosty.Domain.UnitTests.Records;

internal static class RecordData {

    public static readonly Name<Firstname> Fn = new Name<Firstname>("Kev");
    public static readonly Name<Lastname> Ln = new Name<Lastname>("Ize");

    public static readonly ContactInfo ContactInfo = new ContactInfo(Fn, Ln);

    public async static Task<Result<Website>> WebsiteTrue() {
        var res = await Website.Create(
            "test.com", RecordServices.PingTrue);

        return res;
    }

    public async static Task<Result<Website>> WebsiteFalse() {
        var res = await Website.Create(
            "test.com", RecordServices.PingFalse);

        return res;
    }

    public static readonly DateTime CreatedOn = DateTime.Now;
}

internal class RecordSensitiveData {

    public readonly static Task<Result<Website>> WebsiteResult = RecordData.WebsiteTrue();
    public readonly static Result<Website> WebsitePass = WebsiteResult.Result;

    public readonly static Task<Result<Website>> WebsiteResultFail = RecordData.WebsiteFalse();
    public readonly static Result<Website> WebsiteFail = WebsiteResultFail.Result;

    public static readonly Result<Email> EmailAddress = Email.Create(
        "test@gmail.com",
        RecordData.ContactInfo,
        WebsitePass._value
    );

}
