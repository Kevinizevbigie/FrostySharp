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


    public EmailPipelineCard Create(string primaryEmailSubmission,
                                    Record record) {

        MakeRecordReadyToSend(primaryEmailSubmission, record);

        var pipelineCard = new EmailPipelineCard(Guid.NewGuid(), record);

        pipelineCard.AddDomainEvent(new ReadyToSendDomainEvent(record.Id));

        EmailCounter = 0;

        return pipelineCard;
    }

    // Users manually look through verifiedemailguesses and decide
    // which email to add to the primary record
    // this is because we want to ensure email deliverability by
    // adding the verified email we want.
    private Result MakeRecordReadyToSend(string email, Record record) {

        // VERIFICATION
        if (record.LeadStatus != LeadStatus.EmailVerified) {
            return Result.Failure(CardErrors.CannotAddToSendList);
        }

        // First, create an email object
        var emailResult = Email.Create(
            email,
            record.PrimaryContact,
            record.Website
        );

        // Add Email to record
        record.AddEmail(emailResult);
        // add verified email to Card
        RecordEmail = record.PrimaryContact.Email?.Value;
        RecordFirstname = record.PrimaryContact.Firstname.Value;

        // set initial card status
        CardStatus = CardStatus.ReadyToSend;

        // Domain event - make ready to send, this will persis the
        // changes to the record in the DB

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

        if (RecordEntity.LeadStatus != LeadStatus.EmailVerified) {
            return Result.Failure(CardErrors.CannotAddToSendList);
        }

        if (CardStatus == CardStatus.Unsubscribed ||
           RecordEntity.LeadStatus == LeadStatus.Rejected
        ) {
            return Result.Failure(CardErrors.RejectedRecord);
        }

        if (RecordEmail == null ||
            RecordFirstname == null) {
            return Result.Failure(CardErrors.RejectedRecord);
        }

        CardStatus = CardStatus.InitialEmailSent;

        service.Add(RecordFirstname, RecordEmail);

        LogEmail();

        return Result.Success();
    }


    // Directly used by application service
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


