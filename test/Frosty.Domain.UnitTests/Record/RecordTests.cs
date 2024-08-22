using Frosty.Domain.Framework;
using Frosty.Domain.Records.Events;
namespace Frosty.Domain.UnitTests.Records;

public class RecordTests {

    [Fact]
    public async void Create_Should_ReturnFailure_When_DuplicateRecordServiceFails() {
        // If website is duplicate this should fail

        // Act
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

    [Fact]
    public async void Create_Should_ReturnRecord_On_Success() {

        // Act
        var KevRecord = await Frosty.Domain.Records.Record.Create(
            RecordData.Fn,
            RecordData.Ln,
            RecordSensitiveData.WebsitePass,
            RecordData.CreatedOn,
            RecordServices.DupCheckSucceed
        );

        var want = typeof(Frosty.Domain.Records.Record);
        var got = KevRecord._value.GetType();

        // Assert
        Assert.Equal(want, got);
    }

    [Fact]
    public async void Create_Should_RaiseDomainEvent_On_Success() {

        // Act
        var KevRecord = await Frosty.Domain.Records.Record.Create(
            RecordData.Fn,
            RecordData.Ln,
            RecordSensitiveData.WebsitePass,
            RecordData.CreatedOn,
            RecordServices.DupCheckSucceed
        );

        var want = typeof(RecordCreatedDomainEvent);

        // Get record object, get domain events list, read first value
        // then get the type of that first value
        var got = KevRecord._value.GetDomainEvents().First().GetType();

        // Assert
        Assert.Equal(want, got);
    }


    [Fact]
    public void VerifyEmails_Should_ChangeLeadStatusToVerifed() {

        var email = RecordSensitiveData.EmailAddress._value;


    }

    [Fact]
    public void VerifyEmails_Should_AddVerifyEmailResultsToEmailVerifyList() {

    }

    [Fact]
    public void AddToSendQueue_Should_RaiseDomainEvent_On_Success() {


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