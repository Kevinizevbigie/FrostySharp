using Frosty.Domain.Records;
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
    public async void VerifyEmails_Should_Fail_WhenLeadStatusIsNotValid() {

        // Act
        var KevRecord = await Frosty.Domain.Records.Record.Create(
            RecordData.Fn,
            RecordData.Ln,
            RecordSensitiveData.WebsitePass,
            RecordData.CreatedOn,
            RecordServices.DupCheckSucceed
        );

        // Need to change lead status to website success

        var record = KevRecord._value;
        var id = record.Id;

        //Change lead status back to new
        record.ChangeLeadStatus(LeadStatus.New);

        var service = RecordServices.VerifyPass;
        var res = await record.VerifyEmailGuesses(service);

        var want = RecordErrors.UnableToVerify;
        var got = res.Error;

        Assert.Equal(want, got);

    }

    [Fact]
    public async void VerifyEmails_Should_Fail_WhenGuessesAreEmpty() {

        // Act
        var KevRecord = await Frosty.Domain.Records.Record.Create(
            RecordData.Fn,
            RecordData.Ln,
            RecordSensitiveData.WebsitePass,
            RecordData.CreatedOn,
            RecordServices.DupCheckSucceed
        );

        // Need to change lead status to website success

        var record = KevRecord._value;
        var id = record.Id;

        // add email to record
        record.AddEmail(RecordSensitiveData.EmailAddress);

        // Clear the list
        record.PrimaryContact.Email?.EmailGuessList?.Clear();

        var service = RecordServices.VerifyPass;
        var res = await record.VerifyEmailGuesses(service);

        var want = RecordErrors.VerifyListEmpty;
        var got = res.Error;

        Assert.Equal(want, got);
    }

    [Fact]
    public async void VerifyEmails_Should_ChangeLeadStatusToRejected_When_NoPassGuesses() {

        // Act
        var KevRecord = await Frosty.Domain.Records.Record.Create(
            RecordData.Fn,
            RecordData.Ln,
            RecordSensitiveData.WebsitePass,
            RecordData.CreatedOn,
            RecordServices.DupCheckSucceed
        );

        var record = KevRecord._value;
        var id = record.Id;

        // Create email
        var email = Email.Create(
           "test@gmail.com",
           RecordData.ContactInfo,
           RecordSensitiveData.WebsitePass._value);

        // add email to record
        record.AddEmail(email);

        var service = RecordServices.VerifyFail;
        var res = await record.VerifyEmailGuesses(service);

        var want = LeadStatus.Rejected;
        var got = record.LeadStatus;

        // Assert.NotEqual(res, res);
        Assert.Equal(want, got);

    }

    [Fact]
    public async void VerifyEmails_Should_ChangeLeadStatusToVerified_When_OnSuccess() {
        //TODO: Refactor duplication later

        // Act
        var KevRecord = await Frosty.Domain.Records.Record.Create(
            RecordData.Fn,
            RecordData.Ln,
            RecordSensitiveData.WebsitePass,
            RecordData.CreatedOn,
            RecordServices.DupCheckSucceed
        );

        var record = KevRecord._value;
        var id = record.Id;

        // Create email
        var email = Email.Create(
           "test@gmail.com",
           RecordData.ContactInfo,
           RecordSensitiveData.WebsitePass._value);

        // add email to record
        record.AddEmail(email);

        var service = RecordServices.VerifyPass;
        var res = await record.VerifyEmailGuesses(service);

        var want = LeadStatus.EmailVerified;
        var got = record.LeadStatus;

        Assert.Equal(want, got);
    }

    // ======================================== //
    // Value Object Functionality Unit Tests
    // ======================================== //

    [Fact]
    public async void NewWebsite_Should_Fail_When_WebsitePingServiceFails() {


        var pingResult = await Website.Create(
            "test.com",
            RecordServices.PingFalse);

        var want = RecordErrors.WebsiteRejected;
        var got = pingResult.Error;
        Assert.Equal(want, got);

    }

    [Fact]
    public void Email_ReturnFailure_When_EmailStringIsInaccurate() {

        // Create email
        var email = Email.Create(
           "test@gmail",
           RecordData.ContactInfo,
           RecordSensitiveData.WebsitePass._value);

        var want = RecordErrors.BadEmail;
        var got = email.Error;
        Assert.Equal(want, got);
    }

    [Fact]
    public async void Comment_ReturnFailure_When_InfoIsBlank() {

        var KevRecord = await Frosty.Domain.Records.Record.Create(
            RecordData.Fn,
            RecordData.Ln,
            RecordSensitiveData.WebsitePass,
            RecordData.CreatedOn,
            RecordServices.DupCheckSucceed
        );

        var record = KevRecord._value;

        var res = record.AddComment("", "This person is a nice guy");


        var want = false;
        var got = res;
        Assert.Equal(want, got);
    }
}
