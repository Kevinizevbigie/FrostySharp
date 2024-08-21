
using Frosty.Domain.Framework;
using Frosty.Domain.Records;
using Frosty.Domain.Records.Services;
using Frosty.Domain.Shared;

namespace Frosty.Domain.UnitTests.Record;

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


internal class RecordServices {
    private WebsitePingTrue PingTrue = new();
    private WebsitePingFalse PingFalse = new();
}

internal static class RecordData {

    public static readonly Name<Firstname> Fn = new Name<Firstname>("Kev");
    public static readonly Name<Lastname> Ln = new Name<Lastname>("Ize");

    public static readonly ContactInfo ContactInfo = new ContactInfo(Fn, Ln);


    public static readonly Website WebsiteTrue = new Website("test.com");
    public static readonly Website WebsiteFalse = new Website("test.com");

    // public static readonly Email Email = Email.Create(
    //     "test@gmail.com",
    //     ContactInfo,

    // );

}

public class RecordTests {


    [Fact]
    public void Create_Should_ReturnFailure_When_DuplicateRecordServiceFails() {

        // Arrange


        // Act


        // Assert

    }

    [Fact]
    public void Create_Should_ReturnRecord_On_Success() {

        // Arrange



        // Act


        // Assert

    }

    [Fact]
    public void Create_Should_RaiseDomainEvent_On_Success() {

        // Arrange



        // Act


        // Assert

    }

    [Fact]
    public void VerifyEmails_Should_ChangeLeadStatusToVerifed() {

        // Arrange



        // Act


        // Assert

    }

    [Fact]
    public void VerifyEmails_Should_AddVerifyEmailResultsToEmailVerifyList() {

        // Arrange



        // Act


        // Assert

    }

    [Fact]
    public void AddToSendQueue_Should_RaiseDomainEvent_On_Success() {

        // Arrange



        // Act


        // Assert

    }


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
