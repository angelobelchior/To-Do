namespace MediatrPoC.Applications;

public enum ResultStatus
{
    Success,
    HasValidation,
    HasError,
    EntityNotFound,
    EntityAlreadyExists,
    NoContent
}

public class Result
{
    public static Result Success() => new() { _status = ResultStatus.Success };
    public static Result WithNoContent() => new() { _status = ResultStatus.NoContent };
    public static Result EntityNotFound(string entity, object id, string description)
        => new()
        {
            _status = ResultStatus.EntityNotFound,
            _entity = new Entity(entity, id, description)
        };

    public static Result EntityAlreadyExists(string entity, object id, string description)
        => new()
        {
            _status = ResultStatus.EntityAlreadyExists,
            _entity = new Entity(entity, id, description)
        };
    public static Result WithError(string message)
        => new()
        {
            _status = ResultStatus.HasError,
            _error = new Error(message)
        };
    public static Result WithError(Exception exception) => WithError(exception.Message);
    public static Result WithValidations(params Validation[] validations)
    {
        var result = new Result { _status = ResultStatus.HasValidation };
        result._validations.AddRange(validations);
        return result;
    }
    public static Result WithValidations(string entity, string description)
        => WithValidations(new Validation(entity, description));

    protected ResultStatus _status;
    public ResultStatus Status => _status;

    protected List<Validation> _validations = new();
    public IEnumerable<Validation> Validations => _validations;

    protected Error? _error = null;
    public Error? Error => _error;

    protected Entity? _entity = null;
    public Entity? Entity => _entity;
}

public class Result<T> : Result
{
    public static Result<T> Success(T data) => new() { _data = data, _status = ResultStatus.Success };
    public new static Result<T> WithNoContent() => new() { _status = ResultStatus.NoContent };
    public new static Result<T> EntityNotFound(string entity, object id, string description)
        => new()
        {
            _status = ResultStatus.EntityNotFound,
            _entity = new Entity(entity, id, description)
        };
    public new static Result<T> EntityAlreadyExists(string entity, object id, string description)
        => new()
        {
            _status = ResultStatus.EntityAlreadyExists,
            _entity = new Entity(entity, id, description)
        };
    public new static Result<T> WithError(string message)
        => new()
        {
            _status = ResultStatus.HasError,
            _error = new Error(message)
        };
    public new static Result<T> WithError(Exception exception) => WithError(exception.Message);
    public new static Result<T> WithValidations(params Validation[] validations)
    {
        var result = new Result<T> { _status = ResultStatus.HasValidation };
        result._validations.AddRange(validations);
        return result;
    }
    public new static Result<T> WithValidations(string entity, string description)
        => WithValidations(new Validation(entity, description));

    private T? _data;
    public T? Data => _data;
}

public record Validation(string PropertyName, string Description);
public record Error(string Description);
public record Entity(string Name, object Id, string Description);