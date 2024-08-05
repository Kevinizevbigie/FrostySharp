
namespace Frosty.Domain.Framework;

public class Result {

}

public class Result<TVal> {

    public readonly TVal _value;
    public Error Error;
    public bool IsSuccess;

    public bool IsFailure => !IsSuccess;


    private Result(TVal value, bool isSuccess, Error error) {
        _value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    // Create a new success result object which encapsulates
    // the result value, if any. The result status and error
    // object is also encapsulated
    public static Result<TVal> Success(
        TVal value,
        bool isSuccess,
        Error error) {
        return new Result<TVal>(value, isSuccess, error);
    }

    // TODO: Add the failure static function
    // TODO: create the error object with a name and descr.
    // TODO: Need to validate against success with error object not none.
    // TODO: Need to be able to return a result object without a value...? For example, for failed results, we only need the error object, not the value


}
