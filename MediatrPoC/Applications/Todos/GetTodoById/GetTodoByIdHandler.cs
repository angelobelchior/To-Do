namespace MediatrPoC.Applications.Todos.GetTodoById;

public record GetTodoByIdRequest(Guid Id) : IRequest<Result<Todo>>
{
    public class GetTodoByIdRequestValidator : AbstractValidator<GetTodoByIdRequest>
    {
        public GetTodoByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdRequest, Result<Todo>>
{
    private readonly ITodosRepository _repository;
    public GetTodoByIdHandler(ITodosRepository repository)
        => _repository = repository;

    public async Task<Result<Todo>> Handle(GetTodoByIdRequest request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetById(request.Id, cancellationToken);
        if (todo is null) return Result<Todo>.EntityNotFound("ToDo", request.Id, $"To-do {request.Id} not found");

        return Result<Todo>.Success(todo);
    }
}
