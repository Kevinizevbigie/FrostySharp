
using Frosty.Domain.Framework;
using Frosty.Domain.Record;
namespace Frosty.Domain.EmailPipeline;

// Sometimes, in the business, this is called a Email Queue. And sometimes it is called an Email Pipeline. It's simply the records that need to be sent an initial email.

// The reason I picked Pipeline instead of queue is because I want to seperate business thinking from the Queue data Structure.

public class EmailPipeline : Entity {


    public DateTime AddedToPipelineUtc { get; private set; }
    public DateTime RemovedToPipelineUtc { get; private set; }

    public List<Record>? EmailQueue { get; private set; }




    public void Add(Guid recordId) {


    }






}
