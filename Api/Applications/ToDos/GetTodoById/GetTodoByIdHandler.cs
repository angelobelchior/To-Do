namespace ToDo.Api.Applications.Todos.GetTodoById;

public record GetTodoByIdRequest(Guid Id) : IRequest<IResult<Contracts.ToDos.ToDo>>
{
    public class GetTodoByIdRequestValidator : AbstractValidator<GetTodoByIdRequest>
    {
        public GetTodoByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdRequest, IResult<Contracts.ToDos.ToDo>>
{
    private readonly ITodosRepository _repository;
    public GetTodoByIdHandler(ITodosRepository repository)
        => _repository = repository;

    public async Task<IResult<Contracts.ToDos.ToDo>> Handle(GetTodoByIdRequest request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetById(request.Id, cancellationToken);
        if (todo is null) return Result<Contracts.ToDos.ToDo>.EntityNotFound("ToDo", request.Id, $"To-do {request.Id} not found");

        return Result<Contracts.ToDos.ToDo>.Success(todo);
    }
}
