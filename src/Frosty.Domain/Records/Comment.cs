using Frosty.Domain.Framework;
namespace Frosty.Domain.Records;

public sealed record Comment {

    public string Name;
    public string Description;

    private Comment(string name, string desc) {
        Name = name;
        Description = desc;
    }

    public static Result<Comment> Create(string name, string desc) {

        if (string.IsNullOrEmpty(name) ||
            string.IsNullOrEmpty(desc)
        ) {
            return Result.Failure<Comment>(RecordErrors.BlankValue);
        }
        var comment = new Comment(name, desc);

        return Result.Success<Comment>(comment);
    }
}
