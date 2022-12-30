namespace ToDo.Api.Applications.Todos.ListTodos;

public class ListToDosHandler : IRequestHandler<ListToDosQuery, IResult<ListToDosResponse>>
{
    private readonly IToDosReadRepository _readRepository;
    public ListToDosHandler(IToDosReadRepository readRepository)
        => _readRepository = readRepository;

    public async Task<IResult<ListToDosResponse>> Handle(ListToDosQuery request, CancellationToken cancellationToken)
    {
        var todos = await _readRepository.ListAll(request.IsDone, cancellationToken);
        return Result<ListToDosResponse>.Success(new ListToDosResponse(todos));
    }
}
