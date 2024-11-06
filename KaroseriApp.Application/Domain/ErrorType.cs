namespace KaroseriApp.Application.Domain;

public class Error
{
    public string? Code { get; set; }
    public string? ErrorMessage { get; set; }

    public static Error NotFound(string code = "NotFound", string message = "NotFound") =>
        new() { Code = code, ErrorMessage = message };
    public static Error Validation(string code = "Validation", string message = "Validation") =>
        new() { Code = code, ErrorMessage = message };

}

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error? Error { get; }
    public string? Code { get; }
    public string? ErrorMessage { get; }

    protected Result(T value)
    {
        Value = value;
        IsSuccess = true;
        Code = null;
        ErrorMessage = null;
        Error = null;
    }

    private Result(Error error)
    {
        Value = default;
        Code = error.Code;
        ErrorMessage = error.ErrorMessage;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(Error error) => new(error);

    // Implicit conversion from T to Result<T> for Success
    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Error error) => Failure(error);
}
