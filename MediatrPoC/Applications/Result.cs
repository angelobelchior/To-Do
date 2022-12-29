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
    public static Result Success() => new() { Status = ResultStatus.Success };
    public static Result WithNoContent() => new() { Status = ResultStatus.NoContent };
    public static Result EntityNotFound(string entity, object id, string description)
        => new()
        {
            Status = ResultStatus.EntityNotFound,
            Entity = new Entity(entity, id, description)
        };

    public static Result EntityAlreadyExists(string entity, object id, string description)
        => new()
        {
            Status = ResultStatus.EntityAlreadyExists,
            Entity = new Entity(entity, id, description)
        };
    public static Result WithError(string message)
        => new()
        {
            Status = ResultStatus.HasError,
            Error = new Error(message)
        };
    public static Result WithError(Exception exception) => WithError(exception.Message);
    public static Result WithValidations(params Validation[] validations)
        => new()
        {
            Status = ResultStatus.HasValidation,
            Validations = validations
        };
    public static Result WithValidations(string entity, string description)
        => WithValidations(new Validation(entity, description));

    public ResultStatus Status { get; protected set; }

    public IEnumerable<Validation> Validations { get; protected set; } = Enumerable.Empty<Validation>();

    public Error? Error { get; protected set; }

    public Entity? Entity { get; protected set; }
}

public class Result<T> : Result
{
    public static Result<T> Success(T data) => new() { _data = data, Status = ResultStatus.Success };
    public new static Result<T> WithNoContent() => new() { Status = ResultStatus.NoContent };
    public new static Result<T> EntityNotFound(string entity, object id, string description)
        => new()
        {
            Status = ResultStatus.EntityNotFound,
            Entity = new Entity(entity, id, description)
        };
    public new static Result<T> EntityAlreadyExists(string entity, object id, string description)
        => new()
        {
            Status = ResultStatus.EntityAlreadyExists,
            Entity = new Entity(entity, id, description)
        };
    public new static Result<T> WithError(string message)
        => new()
        {
            Status = ResultStatus.HasError,
            Error = new Error(message)
        };
    public new static Result<T> WithError(Exception exception) => WithError(exception.Message);
    public new static Result<T> WithValidations(params Validation[] validations)
        => new()
        {
            Status = ResultStatus.HasValidation,
            Validations = validations
        };
    public new static Result<T> WithValidations(string entity, string description)
        => WithValidations(new Validation(entity, description));

    private T? _data;
    public T? Data => _data;
}

public record Validation(string PropertyName, string Description);
public record Error(string Description);
public record Entity(string Name, object Id, string Description);