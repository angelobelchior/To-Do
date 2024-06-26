namespace ToDo.Api.Presentation;

/// <summary>
/// To-dos
/// </summary>
[ApiController]
[Route("todos")]
[Produces("application/json")]
public class ToDosController : MediatorController
{
    public ToDosController(IMediator mediator)
        : base(mediator) { }

    /// <summary>
    /// Create new to-do
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>New ToDo</returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResult<Contracts.ToDos.ToDo>), StatusCodes.Status201Created)]
    public Task<IActionResult> Post(TodoViewModel request, CancellationToken cancellationToken)
        => Send(new AddNewToDoRequest(request.Title, request.Description, request.IsDone), cancellationToken);

    /// <summary>
    /// Get to-do by Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IResult<Contracts.ToDos.ToDo>), StatusCodes.Status200OK)]
    public Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        => Send(new GetToDoByIdQuery(id), cancellationToken);

    /// <summary>
    /// List all To-dos
    /// </summary>
    /// <param name="isDone">Is Done</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResult<IEnumerable<Contracts.ToDos.ToDo>>), StatusCodes.Status200OK)]
    public Task<IActionResult> Get(bool? isDone = null, CancellationToken cancellationToken = default)
        => Send(new ListToDosQuery(isDone), cancellationToken);

    /// <summary>
    /// Search for a to-do
    /// </summary>
    /// <param name="filter">Filter</param>
    /// <param name="isDone">if Is Done is null, returns all todos.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Todos</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IResult<IEnumerable<Contracts.ToDos.ToDo>>), StatusCodes.Status200OK)]
    public Task<IActionResult> Search(string filter, bool? isDone = null, CancellationToken cancellationToken = default)
        => Send(new SearchToDosQuery(filter, isDone), cancellationToken);

    /// <summary>
    /// Delete a to-do by Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>ToDo</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Contracts.IResult), StatusCodes.Status200OK)]
    public Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        => Send(new DeleteToDoRequest(id), cancellationToken);

    /// <summary>
    /// Update a to-do
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="viewModel">ToDo body</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Result</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(IResult<Contracts.ToDos.ToDo>), StatusCodes.Status200OK)]
    public Task<IActionResult> Put(Guid id, TodoViewModel viewModel, CancellationToken cancellationToken = default)
        => Send(new UpdateToDoRequest(id, viewModel.Title, viewModel.Description, viewModel.IsDone), cancellationToken);

    /// <summary>
    /// Mark a to-do as done
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="viewModel">ToDo body</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Result</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(IResult<Contracts.ToDos.ToDo>), StatusCodes.Status200OK)]
    public Task<IActionResult> Patch(Guid id, MarkAsDoneTodoViewModel viewModel, CancellationToken cancellationToken = default)
        => Send(new MarkToDoAsDoneRequest(id, viewModel.IsDone), cancellationToken);
}
