
namespace Frosty.Domain.Framework;


public class Result<T> {

    public readonly T _value;
    public Error Error;
    public bool IsSuccess;

    public bool IsFailure => !IsSuccess;


    private Result(T value, bool isSuccess, Error error) {
        _value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    // Create a new success result object which encapsulates
    // the result value, if any. The result status and error
    // object is also encapsulated
    public static Result<T> Success(
        T value,
        bool isSuccess,
        Error error) {
        return new Result<T>(value, isSuccess, error);
    }


}
