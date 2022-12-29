namespace ToDo.Api.Applications.Todos.ListTodos;

public record ListTodosRequest(bool? IsDone = null) : IRequest<IResult<ListTodosRequestResponse>>;

public record ListTodosRequestResponse(IEnumerable<Contracts.ToDos.ToDo> Todos);

public class ListTodosHandler : IRequestHandler<ListTodosRequest, IResult<ListTodosRequestResponse>>
{
    private readonly ITodosRepository _addNewTodoRepository;
    public ListTodosHandler(ITodosRepository addNewTodoRepository)
        => _addNewTodoRepository = addNewTodoRepository;

    public async Task<IResult<ListTodosRequestResponse>> Handle(ListTodosRequest request, CancellationToken cancellationToken)
    {
        var todos = await _addNewTodoRepository.ListAll(request.IsDone, cancellationToken);
        return Result<ListTodosRequestResponse>.Success(new ListTodosRequestResponse(todos));
    }
}
