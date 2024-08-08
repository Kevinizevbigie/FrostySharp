
using Frosty.Domain.Framework;
using Frosty.Domain.Record;
namespace Frosty.Domain.EmailPipeline;

// Sometimes, in the business, this is called a Email Queue. And sometimes it is called an Email Pipeline. It's simply the records that need to be sent an initial email.

public class EmailPipeline : Entity {

    public List<Record>? EmailQueue { get; private set; }




    public void Add(Guid recordId) {



    }






}
