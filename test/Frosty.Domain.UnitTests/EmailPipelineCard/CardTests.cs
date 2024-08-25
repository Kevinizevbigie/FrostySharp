
using Frosty.Domain.EmailPipelineCards;
using Frosty.Domain.EmailPipelineCards.Events;
using Frosty.Domain.Framework;
using Frosty.Domain.Records;
using Frosty.Domain.UnitTests.Records;

namespace Frosty.Domain.UnitTests.EmailPipelineCards;

public class CardTests {

    [Fact]
    public async void CreateCard_Should_ReturnFailure_When_LeadStatusIsNotVerified() {

        var record = await CardData.MakeRecord();


        await record.VerifyEmailGuesses(RecordServices.VerifyPass);

        // add dummy email email to record
        record.AddEmail(RecordSensitiveData.EmailAddress);

        // change lead status to an incorrect one
        record.ChangeLeadStatus(LeadStatus.WebsiteValid);

        var Card = EmailPipelineCard.Create(
            record.PrimaryContact.Email?.Value,
            record
        );

        var want = CardErrors.CannotAddToSendList;
        var got = Card.Error;

        Assert.Equal(want, got);

    }

    [Fact]
    public async void CreateCard_Should_ReturnSuccess() {

        var record = await CardData.MakeRecord();

        await record.VerifyEmailGuesses(RecordServices.VerifyPass);

        // add email to record
        record.AddEmail(RecordSensitiveData.EmailAddress);

        var Card = EmailPipelineCard.Create(
            record.PrimaryContact.Email?.Value,
            record
        );

        var want = Error.None;
        var got = Card.Error;

        Assert.Equal(want, got);

    }



}
