namespace MediatrPoC.Applications.Todos.ListTodos;

public record ListTodosRequest(bool? IsDone = null) : IRequest<Result<ListTodosRequestResponse>>;

public record ListTodosRequestResponse(IEnumerable<Todo> Todos);

public class ListTodosHandler : IRequestHandler<ListTodosRequest, Result<ListTodosRequestResponse>>
{
    private readonly ITodosRepository _addNewTodoRepository;
    public ListTodosHandler(ITodosRepository addNewTodoRepository)
        => _addNewTodoRepository = addNewTodoRepository;

    public async Task<Result<ListTodosRequestResponse>> Handle(ListTodosRequest request, CancellationToken cancellationToken)
    {
        var todos = await _addNewTodoRepository.ListAll(request.IsDone, cancellationToken);
        return Result<ListTodosRequestResponse>.Success(new ListTodosRequestResponse(todos));
    }
}
