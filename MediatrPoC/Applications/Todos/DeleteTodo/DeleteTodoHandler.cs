namespace MediatrPoC.Applications.Todos.DeleteTodo;

public record DeleteTodoRequest(Guid Id) : IRequest<Result>
{
    public class DeleteTodoRequestValidator : AbstractValidator<DeleteTodoRequest>
    {
        public DeleteTodoRequestValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }
}

public record DeleteTodoNotification() : NotificationBase;

public class DeleteTodoHandler : IRequestHandler<DeleteTodoRequest, Result>
{
    private readonly IMediator _mediator;
    private readonly ITodosRepository _repository;
    public DeleteTodoHandler(IMediator mediator, ITodosRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteTodoRequest request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetById(request.Id, cancellationToken);
        if (todo is null) return Result.EntityNotFound("ToDo", request.Id, $"To-do {request.Id} not found");

        await _repository.Delete(request.Id, cancellationToken);
        await _mediator.Publish(new DeleteTodoNotification
        {
            UniqueId = request.Id,
            Application = "Todo",
            Feature = "DeleteTodo"
        }, cancellationToken);
        
        return Result.Success();
    }
}
