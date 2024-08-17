using Frosty.Domain.EmailPipelineCards.Events;
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

        if (record.PrimaryContact.Email == null ||
            record.PrimaryContact.Firstname == null) {
            // return exception
        }


        RecordId = record.Id;
        RecordFirstname = record.PrimaryContact.Firstname;
        RecordEmail = record.PrimaryContact.Email;
    }

    public Guid RecordId { get; private set; }
    public string? RecordFirstname { get; private set; }
    public string? RecordEmail { get; private set; }

    public DateTime AddedToPipelineUtc { get; private set; }
    public DateTime RemovedToPipelineUtc { get; private set; }

    public CardStatus CardStatus { get; private set; }

    public List<EmailLog>? EmailLogs { get; private set; }
    public int? EmailCounter { get; private set; }

    // will run via event handler
    // TODO: I need a way to prevent a duplicate card
    // inside repository for EmailPipelineCard 
    // In command, we check for duplicate before we run "add to pipeline"
    // return result - failure or success
    public void AddToPipeline(Guid recordId) {

        if (CardStatus == CardStatus.Rejected) {
            // return exception/error
        }

        AddedToPipelineUtc = DateTime.UtcNow;
        CardStatus = CardStatus.ReadyToSend;
    }

    public void FirstEmailSentStatusChange() {
        CardStatus = CardStatus.InitialEmailSent;

        AddDomainEvent(new InitialEmailSentDomainEvent(Id));

    }

    public void RemoveRecordFromSendingList() {
        CardStatus = CardStatus.Unsubscribed;

        AddDomainEvent(new UnsubscribedContactDomainEvent(Id));
    }

}


