namespace MediatrPoC.Applications.Todos.ListTodos;

public record ListTodosRequest(bool? IsDone = null) : IRequest<ListTodosRequestResponse>;

public record ListTodosRequestResponse(IEnumerable<Todo> Todos);

public class ListTodosHandler : IRequestHandler<ListTodosRequest, ListTodosRequestResponse>
{
    private readonly ITodosRepository _addNewTodoRepository;
    public ListTodosHandler(ITodosRepository addNewTodoRepository)
        => _addNewTodoRepository = addNewTodoRepository;

    public async Task<ListTodosRequestResponse> Handle(ListTodosRequest request, CancellationToken cancellationToken)
    {
        var todos = await _addNewTodoRepository.ListAll(request.IsDone);
        return new ListTodosRequestResponse(todos);
    }
}
