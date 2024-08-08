
using Frosty.Domain.Framework;
using Frosty.Domain.Records;
namespace Frosty.Domain.EmailPipeline;

// Sometimes, in the business, this is called a Email Queue. And sometimes it is called an Email Pipeline. It's simply the records that need to be sent an initial email.

// The reason I picked Pipeline instead of queue is because I want to seperate business thinking from the Queue data Structure.

public class EmailPipelineCard : Entity {

    private EmailPipelineCard(
        Guid id,
        Record record
    ) : base(id) {

    }

    public string RecordFirstname { get; private set; }
    public string RecordEmail { get; private set; }

    public DateTime AddedToPipelineUtc { get; private set; }
    public DateTime RemovedToPipelineUtc { get; private set; }

    public CardStatus CardStatus { get; private set; }

    public List<Record>? EmailQueue { get; private set; }


    // 
    public void AddToPipeline(Guid recordId) {

        // set added time
        // set card status
        // create new card


    }
}


public enum CardStatus {
    NotContacted = 1,
    InitialEmailSent = 2,
    MultipleContactsSent = 3,
    Rejected = 4
}
