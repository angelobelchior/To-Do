namespace ToDo.Api.Application.ToDos.GetTodoById;

public class GetToDoByIdRequestValidator : AbstractValidator<GetToDoByIdQuery>
{
    public GetToDoByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public class GetToDoByIdHandler : IRequestHandler<GetToDoByIdQuery, IResult<Contracts.ToDos.ToDo>>
{
    private readonly IToDosReadRepository _readRepository;
    public GetToDoByIdHandler(IToDosReadRepository readRepository)
        => _readRepository = readRepository;

    public async Task<IResult<Contracts.ToDos.ToDo>> Handle(GetToDoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await _readRepository.GetById(request.Id, cancellationToken);
        if (todo is null) return Result<Contracts.ToDos.ToDo>.EntityNotFound("ToDo", request.Id, $"To-do {request.Id} not found");

        return Result<Contracts.ToDos.ToDo>.Success(todo);
    }
}
