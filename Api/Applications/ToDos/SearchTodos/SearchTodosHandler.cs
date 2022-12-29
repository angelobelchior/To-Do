namespace ToDo.Api.Applications.Todos.SearchTodos;

public record SearchTodosRequest(string Filter, bool? IsDone = null) : IRequest<IResult<SearchTodosRequestResponse>>;

public record SearchTodosRequestResponse(IEnumerable<Contracts.ToDos.ToDo> Todos);

public class SearchTodosHandler : IRequestHandler<SearchTodosRequest, IResult<SearchTodosRequestResponse>>
{
    private readonly ITodosRepository _addNewTodoRepository;
    public SearchTodosHandler(ITodosRepository addNewTodoRepository)
        => _addNewTodoRepository = addNewTodoRepository;

    public async Task<IResult<SearchTodosRequestResponse>> Handle(SearchTodosRequest request, CancellationToken cancellationToken)
    {
        var todos = await _addNewTodoRepository.Search(request.Filter, request.IsDone, cancellationToken);
        return Result<SearchTodosRequestResponse>.Success(new SearchTodosRequestResponse(todos));
    }
}
