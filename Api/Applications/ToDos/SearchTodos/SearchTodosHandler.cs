namespace ToDo.Api.Applications.Todos.SearchTodos;

public class SearchToDosHandler : IRequestHandler<SearchToDosQuery, IResult<SearchToDosResponse>>
{
    private readonly IToDosReadRepository _readRepository;
    public SearchToDosHandler(IToDosReadRepository readRepository)
        => _readRepository = readRepository;

    public async Task<IResult<SearchToDosResponse>> Handle(SearchToDosQuery request, CancellationToken cancellationToken)
    {
        var todos = await _readRepository.Search(request.Filter, request.IsDone, cancellationToken);
        return Result<SearchToDosResponse>.Success(new SearchToDosResponse(todos));
    }
}
