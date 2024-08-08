
using Frosty.Domain.Framework;
using Frosty.Domain.Records;
namespace Frosty.Domain.EmailPipeline;

// Sometimes, in the business, this is called a Email Queue. And sometimes it is called an Email Pipeline. It's simply the records that need to be sent an initial email.

// The reason I picked Pipeline instead of queue is because I want to seperate business thinking from the Queue data Structure.

public class EmailPipeline : Entity {

    private EmailPipeline(
        Guid id,
        Record record
    ) : base(id) {

    }

    public string RecordFirstname { get; private set; }
    public string RecordEmail { get; private set; }

    public DateTime AddedToPipelineUtc { get; private set; }
    public DateTime RemovedToPipelineUtc { get; private set; }

    public List<Record>? EmailQueue { get; private set; }




    public void AddPipelineCard(Guid recordId) {

        // set added time
        // create new object - builder

        // I need

    }






}
