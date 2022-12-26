namespace MediatrPoC.Controllers;

[ApiController]
[Route("todos")]
public class TodosController : MediatorController
{
    public TodosController(ILogger<TodosController> logger, IMediator mediator)
        : base(logger, mediator) { }
    
    /// <summary>
    /// Create new Todo
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>New Todo</returns>
    [HttpPost]
    public Task<IActionResult> Post(TodoViewModel request, CancellationToken cancellationToken)
        => Send(new AddNewTodoRequest(request.Title, request.Description, request.IsDone), cancellationToken);

    /// <summary>
    /// List all Todos
    /// </summary>
    /// <param name="isDone">Is Done</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    [HttpGet]
    public Task<IActionResult> Get(bool? isDone = null, CancellationToken cancellationToken = default)
        => Send(new ListTodosRequest(isDone), cancellationToken);

    /// <summary>
    /// Search for a new task
    /// </summary>
    /// <param name="filter">Filter</param>
    /// <param name="isDone">if Is Done is null, returns all todos.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Todos</returns>
    [HttpGet("search")]
    public Task<IActionResult> Search(string filter, bool? isDone = null, CancellationToken cancellationToken = default)
    => Send(new SearchTodosRequest(filter, isDone), cancellationToken);

    /// <summary>
    /// Get a Todo by Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Todo</returns>
    [HttpDelete("{id}")]
    public Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        => Send(new DeleteTodoRequest(id), cancellationToken);

    /// <summary>
    /// Update a todo
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="viewModel">Todo body</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Result</returns>
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, TodoViewModel viewModel, CancellationToken cancellationToken = default)
        => Send(new UpdateTodoRequest(id, viewModel.Title, viewModel.Description, viewModel.IsDone), cancellationToken);

    /// <summary>
    /// Mark a todo as done
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="viewModel">Todo body</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Result</returns>
    [HttpPatch("{id}")]
    public Task<IActionResult> Patch(Guid id, MarkAsDoneTodoViewModel viewModel, CancellationToken cancellationToken = default)
        => Send(new MarkAsDoneTodoRequest(id, viewModel.IsDone), cancellationToken);
}
