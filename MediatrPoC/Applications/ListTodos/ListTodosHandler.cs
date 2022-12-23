using MediatrPoC.Applications.AddNewTodo;

namespace MediatrPoC.Applications.ListTodos;

public record ListTodosRequest() : IRequest<ListTodosRequestResponse>;

public record ListTodosRequestResponse(IEnumerable<Todo> Todos);

public class ListTodosHandler : IRequestHandler<ListTodosRequest, ListTodosRequestResponse>
{
    private readonly IMediator _mediator;
    private readonly ITodoRepository _addNewTodoRepository;
    public ListTodosHandler(IMediator mediator, ITodoRepository addNewTodoRepository)
    {
        _mediator = mediator;
        _addNewTodoRepository = addNewTodoRepository;
    }

    public async Task<ListTodosRequestResponse> Handle(ListTodosRequest request, CancellationToken cancellationToken)
    {
        var todos = await _addNewTodoRepository.ListAll();
        return new ListTodosRequestResponse(todos);
    }
}
