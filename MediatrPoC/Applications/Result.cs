namespace MediatrPoC.Applications;

public enum ResultStatus
{
    Success,
    HasValidation,
    HasError,
    EntityNotFound,
    NoContent
}

public class Result
{
    public static Result Success() => new() { _status = ResultStatus.Success };
    public static Result NoContent() => new() { _status = ResultStatus.NoContent };
    public static Result EntityNotFound(string entity, string description)
        => new()
        {
            _status = ResultStatus.EntityNotFound,
            _validations = new List<Validation> { new Validation(entity, description) }
        };
    public static Result Error(string message)
        => new()
        {
            _status = ResultStatus.HasError,
            _errors = new List<Error> { new Error(message) }
        };
    public static Result Error(Exception exception) => Error(exception.Message);
    public static Result Validation(params Validation[] validations)
    {
        var result = new Result { _status = ResultStatus.HasValidation };
        result._validations.AddRange(validations);
        return result;
    }
    public static Result Validation(string entity, string description)
        => Validation(new Validation(entity, description));

    protected ResultStatus _status;
    public ResultStatus Status => _status;

    protected List<Validation> _validations = new();
    public IEnumerable<Validation> Validations => _validations;

    protected List<Error> _errors = new();
    public IEnumerable<Error> Errors => _errors;
}

public class Result<T> : Result
{
    public static Result<T> Success(T data) => new() { _data = data, _status = ResultStatus.Success };
    public new static Result<T> NoContent() => new() { _status = ResultStatus.NoContent };
    public new static Result<T> EntityNotFound(string entity, string description)
        => new()
        {
            _status = ResultStatus.EntityNotFound,
            _validations = new List<Validation> { new Validation(entity, description) }
        };
    public new static Result<T> Error(string message)
        => new()
        {
            _status = ResultStatus.HasError,
            _errors = new List<Error> { new Error(message) }
        };
    public new static Result<T> Error(Exception exception) => Error(exception.Message);
    public new static Result<T> Validation(params Validation[] validations)
    {
        var result = new Result<T> { _status = ResultStatus.HasValidation };
        result._validations.AddRange(validations);
        return result;
    }
    public new static Result<T> Validation(string entity, string description)
        => Validation(new Validation(entity, description));

    private T? _data;
    public T? Data => _data;
}

public record Validation(string PropertyName, string Description);
public record Error(string Description);