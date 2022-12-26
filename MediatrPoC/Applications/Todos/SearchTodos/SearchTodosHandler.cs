namespace MediatrPoC.Applications.Todos.SearchTodos;

public record SearchTodosRequest(string Filter, bool? IsDone = null) : IRequest<SearchTodosRequestResponse>;

public record SearchTodosRequestResponse(IEnumerable<Todo> Todos);

public class SearchTodosHandler : IRequestHandler<SearchTodosRequest, SearchTodosRequestResponse>
{
    private readonly ITodosRepository _addNewTodoRepository;
    public SearchTodosHandler(ITodosRepository addNewTodoRepository)
        => _addNewTodoRepository = addNewTodoRepository;

    public async Task<SearchTodosRequestResponse> Handle(SearchTodosRequest request, CancellationToken cancellationToken)
    {
        var todos = await _addNewTodoRepository.Search(request.Filter, request.IsDone);
        return new SearchTodosRequestResponse(todos);
    }
}
