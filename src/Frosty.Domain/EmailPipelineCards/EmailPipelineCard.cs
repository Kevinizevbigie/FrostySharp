using Frosty.Domain.EmailPipelineCards.Events;
using Frosty.Domain.EmailPipelineCards.Services;
using Frosty.Domain.Framework;
using Frosty.Domain.Records;

namespace Frosty.Domain.EmailPipelineCards;

// Sometimes, in the business, this is called a Email Queue. And sometimes it is called an Email Pipeline. It's simply the records that need to be sent an initial email.

// The reason I picked Pipeline instead of queue is because I want to seperate business thinking from the Queue data Structure.

public class EmailPipelineCard : Entity {

    private EmailPipelineCard(
        Guid id,
        Record record
    ) : base(id) {


        RecordEntity = record;
    }

    public Record RecordEntity { get; private set; }
    public string? RecordFirstname { get; private set; }
    public string? RecordEmail { get; private set; }

    public DateTime AddedToPipelineUtc { get; private set; }
    public DateTime RemovedToPipelineUtc { get; private set; }

    public CardStatus CardStatus { get; private set; }

    public List<EmailLog> EmailLogs = new();

    public int EmailCounter { get; private set; }

    public static Result<EmailPipelineCard> Create(
        string primaryEmailSubmission,
        Record record) {

        var pipelineCard = new EmailPipelineCard(Guid.NewGuid(), record);

        var res =
            pipelineCard.MakeRecordReadyToSend(record);

        if (res.IsSuccess == false) {
            return Result.Failure<EmailPipelineCard>(res.Error);
        }

        pipelineCard.AddDomainEvent(new ReadyToSendDomainEvent(record.Id));

        pipelineCard.EmailCounter = 0;

        return Result.Success<EmailPipelineCard>(pipelineCard);
    }

    // There will be one service for AddEmail function in the record
    // This function will read that email from the record after that.
    private Result MakeRecordReadyToSend(Record record) {

        // VERIFICATION
        if (record.LeadStatus != LeadStatus.EmailVerified ||
            string.IsNullOrEmpty(record.PrimaryContact.Email.Value)
        ) {
            return Result.Failure(CardErrors.CannotAddToSendList);
        }

        // First, create an email object
        var emailResult = Email.Create(
            record.PrimaryContact.Email.Value,
            record.PrimaryContact,
            record.Website
        );

        // Add Email to record
        record.AddEmail(emailResult);
        // add verified email to Card
        RecordEmail = record.PrimaryContact.Email?.Value;
        RecordFirstname = record.PrimaryContact.Firstname.Value;

        CardStatus = CardStatus.ReadyToSend;

        return Result.Success();
    }

    // Email Sending it'self is currently managed via a RabbitMQ
    // Service. This function sets the database record to "sending mode"
    // to be picked up by FrostySender (The sending microservice)
    public Result AddRecordToSendingQueue(IAddToSendQueueService service) {
        // Function - Send Email
        if (EmailCounter > 0 ||
            CardStatus == CardStatus.InitialEmailSent
        ) {
            // cannot send initial email -- already sent
            return Result.Failure(CardErrors.InitialEmailAlreadySent);
        }

        if (CardStatus == CardStatus.Unsubscribed ||
            RecordEntity.LeadStatus == LeadStatus.Rejected ||
            CardStatus != CardStatus.ReadyToSend

        ) {
            return Result.Failure(CardErrors.RejectedRecord);
        }

        if (RecordEmail == null ||
            RecordFirstname == null) {
            return Result.Failure(CardErrors.RejectedRecord);
        }

        service.Add(RecordFirstname, RecordEmail);

        CardStatus = CardStatus.InitialEmailSent;

        LogEmail();

        return Result.Success();
    }

    public Result ChangeCardStatus(CardStatus cs) {

        if (CardStatus == CardStatus.Unsubscribed) {
            return Result.Failure(CardErrors.RejectedRecord);
        }

        CardStatus = cs;
        return Result.Success();
    }

    public void UnsubscribeRecord() {
        CardStatus = CardStatus.Unsubscribed;

        AddDomainEvent(new UnsubscribedContactDomainEvent(Id));
    }

    private void LogEmail() {
        var log = new EmailLog(DateTime.Now);
        EmailLogs.Add(log);
        EmailCounter++;
    }

}


