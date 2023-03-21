namespace ToDo.Contracts.ToDos;

/// <summary>
/// To-Do
/// </summary>
/// <param name="Id">Id</param>
/// <param name="Title">Title</param>
/// <param name="Description">Description</param>
/// <param name="IsDone">Is Done</param>
public record ToDo(Guid Id, string Title, string Description, bool IsDone);

/// <summary>
/// Add new To-Do command Request
/// </summary>
/// <param name="Title">Title</param>
/// <param name="Description">Description</param>
/// <param name="IsDone">Is Done</param>
public record AddNewToDoRequest(string Title, string Description, bool IsDone) : IRequest<IResult<AddNewToDoResponse>>
{
    public ToDo ToModel()
        => new(Guid.NewGuid(), Title, Description, IsDone);
}

/// <summary>
/// Add new To-Do command Response
/// </summary>
/// <param name="ToDo"></param>
public record AddNewToDoResponse(ToDo ToDo);

/// <summary>
/// Update To-Do command Request
/// </summary>
/// <param name="Id">Id</param>
/// <param name="Title">Title</param>
/// <param name="Description">Description</param>
/// <param name="IsDone">Is Done</param>
public record UpdateToDoRequest(Guid Id, string Title, string Description, bool IsDone) : IRequest<IResult>;

/// <summary>
/// Mark a To-Do as Done command Request
/// </summary>
/// <param name="Id">Id</param>
/// <param name="IsDone">Is Done</param>
public record MarkToDoAsDoneRequest(Guid Id, bool IsDone) : IRequest<IResult>;

/// <summary>
/// Delete To-Do command Request
/// </summary>
/// <param name="Id">Id</param>
public record DeleteToDoRequest(Guid Id) : IRequest<IResult>;

/// <summary>
/// Get To-Do by Id query Request
/// </summary>
/// <param name="Id"></param>
public record GetToDoByIdQuery(Guid Id) : IRequest<IResult<ToDo>>;

/// <summary>
/// Search To-Dos query Request
/// </summary>
/// <param name="Filter">Filter</param>
/// <param name="IsDone">Is Done</param>
public record SearchToDosQuery(string Filter, bool? IsDone = null) : IRequest<IResult<SearchToDosResponse>>;

/// <summary>
/// Search To-Dos query Response
/// </summary>
/// <param name="Todos"></param>
public record SearchToDosResponse(IEnumerable<ToDo> Todos);

/// <summary>
/// List all To-Dos
/// </summary>
/// <param name="IsDone">Is Done</param>
public record ListToDosQuery(bool? IsDone = null) : IRequest<IResult<ListToDosResponse>>;

/// <summary>
/// List all To-dos Response
/// </summary>
/// <param name="Todos">To-dos List</param>
public record ListToDosResponse(IEnumerable<ToDo> Todos);