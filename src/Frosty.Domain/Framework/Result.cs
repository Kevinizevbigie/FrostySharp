namespace Frosty.Domain.Framework;

public class Result {

    public Error Error;
    public bool IsSuccess;

    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, Error error) {

        // error must be none if success is true
        if (isSuccess == true && error != Error.None) {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    // return a success result without a value
    public static Result Success() {
        return new Result(true, Error.None);
    }

    public static Result Failure(Error error) {
        return new Result(false, error);
    }

    // Create a new success result object which encapsulates
    // the result value, if any. The result status and error
    // object is also encapsulated
    public static Result<TVal> Success<TVal>(TVal value) {
        return new Result<TVal>(value, true, Error.None);
    }

    public static Result<TVal> Failure<TVal>(Error error) {
        return new Result<TVal>(default, false, error);
    }

}

public class Result<TVal> : Result {

    public readonly TVal? _value;

    protected internal Result(
        TVal? value,
        bool isSuccess,
        Error error) : base(isSuccess, error) {

        _value = value;

    }
}
