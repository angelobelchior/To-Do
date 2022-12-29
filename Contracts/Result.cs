namespace ToDo.Contracts;

/// <summary>
/// Result Status
/// </summary>
public enum ResultStatus
{
    /// <summary>
    /// Indicates that the command was executed successfully
    /// </summary>
    Success,
    /// <summary>
    /// Indicates that the command contains validations to be done
    /// </summary>
    HasValidation,
    /// <summary>
    /// Indicates that the command encountered an error on execution
    /// </summary>
    HasError,
    /// <summary>
    /// Indicates that the command did not find a specific entity
    /// </summary>
    EntityNotFound,
    /// <summary>
    /// Indicates that the command is trying to create an entity that already exists
    /// </summary>
    EntityAlreadyExists,
    /// <summary>
    /// Indicates that the command was executed successfully but does not contain any content to be returned
    /// </summary>
    NoContent
}

public interface IResult
{
    ResultStatus Status { get; }
}

public interface IResult<T>
{
    T? Data { get; }
}

public interface IHasValidation
{
    IEnumerable<Validation> Validations { get; }
}

public interface IHasError
{
    Error? Error { get; }
}

public interface IHasEntityWarning
{
    EntityWarning? EntityWarning { get; }
}

public class Result : IResult, IHasValidation, IHasError, IHasEntityWarning
{
    public static IResult Success() => new Result { Status = ResultStatus.Success };
    public static IResult WithNoContent() => new Result { Status = ResultStatus.NoContent };
    public static IResult EntityNotFound(string entity, object id, string description)
        => new Result
        {
            Status = ResultStatus.EntityNotFound,
            EntityWarning = new EntityWarning(entity, id, description)
        };
    public static IResult EntityAlreadyExists(string entity, object id, string description)
        => new Result
        {
            Status = ResultStatus.EntityAlreadyExists,
            EntityWarning = new EntityWarning(entity, id, description)
        };
    public static IResult WithError(string message)
        => new Result
        {
            Status = ResultStatus.HasError,
            Error = new Error(message)
        };
    public static IResult WithError(Exception exception) => WithError(exception.Message);
    public static IResult WithValidations(params Validation[] validations)
        => new Result
        {
            Status = ResultStatus.HasValidation,
            Validations = validations
        };
    public static IResult WithValidations(string entity, string description)
        => WithValidations(new Validation(entity, description));

    /// <summary>
    /// Result Status
    /// </summary>
    public ResultStatus Status { get; protected set; }

    /// <summary>
    /// Validations List
    /// </summary>
    public IEnumerable<Validation> Validations { get; protected set; } = Enumerable.Empty<Validation>();

    /// <summary>
    /// Error Object
    /// </summary>
    public Error? Error { get; protected set; }

    /// <summary>
    /// Entity Warning Object
    /// </summary>
    public EntityWarning? EntityWarning { get; protected set; }
}

public class Result<T> : Result, IResult<T>
{
    public static IResult<T> Success(T data) => new Result<T> { Data = data, Status = ResultStatus.Success };
    public new static IResult<T> WithNoContent() => new Result<T> { Status = ResultStatus.NoContent };
    public new static IResult<T> EntityNotFound(string entity, object id, string description)
        => new Result<T>
        {
            Status = ResultStatus.EntityNotFound,
            EntityWarning = new EntityWarning(entity, id, description)
        };
    public new static IResult<T> EntityAlreadyExists(string entity, object id, string description)
        => new Result<T>
        {
            Status = ResultStatus.EntityAlreadyExists,
            EntityWarning = new EntityWarning(entity, id, description)
        };
    public new static IResult<T> WithError(string message)
        => new Result<T>
        {
            Status = ResultStatus.HasError,
            Error = new Error(message)
        };
    public new static IResult<T> WithError(Exception exception) => WithError(exception.Message);
    public new static IResult<T> WithValidations(params Validation[] validations)
        => new Result<T>
        {
            Status = ResultStatus.HasValidation,
            Validations = validations
        };
    public new static IResult<T> WithValidations(string entity, string description)
        => WithValidations(new Validation(entity, description));

    /// <summary>
    /// Data
    /// </summary>
    public T? Data { get; private set; }
}

/// <summary>
/// Validation
/// </summary>
/// <param name="PropertyName">Property Name</param>
/// <param name="Description">Description</param>
public record Validation(string PropertyName, string Description);

/// <summary>
/// Error
/// </summary>
/// <param name="Description">Description</param>
public record Error(string Description);

/// <summary>
/// Entity Warning
/// </summary>
/// <param name="Name">Entity Name</param>
/// <param name="Id">Entity Id</param>
/// <param name="Message">Message</param>
public record EntityWarning(string Name, object Id, string Message);