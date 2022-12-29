namespace MediatrPoC.Applications.Todos.UpdateTodo;

public record UpdateTodoRequest(Guid Id, string Title, string Description, bool IsDone) : IRequest<Result>
{
    public class UpdateTodoRequestValidator : AbstractValidator<UpdateTodoRequest>
    {
        public UpdateTodoRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100).MinimumLength(3);
            RuleFor(x => x.Description).MaximumLength(800).MinimumLength(3);
        }
    }
}

public record UpdateTodoNotification : NotificationBase;

public class UpdateTodoHandler : IRequestHandler<UpdateTodoRequest, Result>
{
    private readonly IMediator _mediator;
    private readonly ITodosRepository _repository;
    public UpdateTodoHandler(IMediator mediator, ITodosRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateTodoRequest request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetById(request.Id, cancellationToken);
        if (todo is null) return Result.EntityNotFound("ToDo", request.Id, $"To-do {request.Id} not found");

        var update = todo with { Title = request.Title, Description = request.Description, IsDone = request.IsDone };
        await _repository.Update(update, cancellationToken);
        await _mediator.Publish(new UpdateTodoNotification
        {
            UniqueId = request.Id,
            Application = "Todo",
            Feature = "UpdateTodo"
        }, cancellationToken);

        return Result.Success();
    }
}
