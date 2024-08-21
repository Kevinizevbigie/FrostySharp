
using Frosty.Domain.Framework;
using Frosty.Domain.Records;
using Frosty.Domain.Records.Services;
using Frosty.Domain.Shared;

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


internal static class RecordServices {
    internal static WebsitePingTrue PingTrue = new();
    internal static WebsitePingFalse PingFalse = new();
    internal static DuplicateCheckServiceFailed DupCheckFail = new();
    internal static DuplicateCheckServiceSuccess DupCheckSucceed = new();
}

internal static class RecordData {

    public static readonly Name<Firstname> Fn = new Name<Firstname>("Kev");
    public static readonly Name<Lastname> Ln = new Name<Lastname>("Ize");

    public static readonly ContactInfo ContactInfo = new ContactInfo(Fn, Ln);

    public async static Task<Website> WebsiteTrue() {
        var res = await Website.Create(
            "test.com", RecordServices.PingTrue);

        return res._value;
    }

    public async static Task<Website> WebsiteFalse() {
        var res = await Website.Create(
            "test.com", RecordServices.PingFalse);

        return res._value;
    }

    public static readonly DateTime CreatedOn = DateTime.Now;
}

internal class RecordSensitiveData {

    public readonly static Task<Website> WebsiteResult = RecordData.WebsiteTrue();
    public readonly static Website WebsitePass = WebsiteResult.Result;

    public readonly Result<Email> EmailAddress = Email.Create(
        "test@gmail.com",
        RecordData.ContactInfo,
        WebsitePass
    );

}

public class RecordTests {


    [Fact]
    public async void Create_Should_ReturnFailure_When_DuplicateRecordServiceFails() {

        // Act

        // If website is duplicate this should fail
        var KevRecord = await Frosty.Domain.Records.Record.Create(
            RecordData.Fn,
            RecordData.Ln,
            RecordSensitiveData.WebsitePass,
            RecordData.CreatedOn,
            RecordServices.DupCheckFail
        );

        var want = true;
        var got = KevRecord.IsFailure;

        // Assert
        Assert.Equal(want, got);

    }

    // [Fact]
    // public void Create_Should_ReturnRecord_On_Success() {

    // Arrange



    // Act


    // Assert

    // }

    // [Fact]
    // public void Create_Should_RaiseDomainEvent_On_Success() {

    // Arrange



    // Act


    // Assert

    // }

    // [Fact]
    // public void VerifyEmails_Should_ChangeLeadStatusToVerifed() {

    // Arrange



    // Act


    // Assert

    // }

    // [Fact]
    // public void VerifyEmails_Should_AddVerifyEmailResultsToEmailVerifyList() {

    // Arrange



    // Act


    // Assert

    // }

    // [Fact]
    // public void AddToSendQueue_Should_RaiseDomainEvent_On_Success() {

    // Arrange



    // Act


    // Assert

    // }


    // ======================================== //
    // Value Object Functionality Unit Tests
    // ======================================== //

    // Website Create new (Factory)
    // Email Create new (Factory)
    // Email Verify Email Address
    // Email Create list guesses are accurate
    // Email Create list guesses are are added to email prop

    // Email verify id random id created

    // New comment created

    // ======================================== //
    // Tests to add later
    // ======================================== //

    // Record rejected via domain event
}
