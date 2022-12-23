

namespace MediatrPoC.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : MediatorController
{
    public TodoController(ILogger<TodoController> logger, IMediator mediator) 
        : base(logger, mediator)
    {
    }

    [HttpPost]
    public Task<IActionResult> Post(AddNewTodoRequest request, CancellationToken cancellationToken)
        => Send(request, cancellationToken);

    [HttpGet]
    public Task<IActionResult> Get(CancellationToken cancellationToken)
    => Send(new ListTodosRequest(), cancellationToken);
}
    