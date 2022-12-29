namespace ToDo.Api.Applications.Todos.MarkAsDoneTodo;

public record MarkAsDoneTodoRequest(Guid Id, bool IsDone) : IRequest<Contracts.IResult>
{
    public class MarkAsDoneTodoRequestValidator : AbstractValidator<MarkAsDoneTodoRequest>
    {
        public MarkAsDoneTodoRequestValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }
}

public record MarkAsDoneTodoNotification : NotificationBase;

public class MarkAsDoneTodoHandler : IRequestHandler<MarkAsDoneTodoRequest, Contracts.IResult>
{
    private readonly IMediator _mediator;
    private readonly ITodosRepository _repository;
    public MarkAsDoneTodoHandler(IMediator mediator, ITodosRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<Contracts.IResult> Handle(MarkAsDoneTodoRequest request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetById(request.Id, cancellationToken);
        if (todo is null) return Result.EntityNotFound("ToDo", request.Id, $"To-do {request.Id} not found");

        var update = todo with { IsDone = request.IsDone };

        await _repository.Update(update, cancellationToken);
        await _mediator.Publish(new MarkAsDoneTodoNotification
        {
            UniqueId = request.Id,
            Application = "ToDo",
            Feature = "MarkAsDoneTodo"
        }, cancellationToken);

        return Result.Success();
    }
}
