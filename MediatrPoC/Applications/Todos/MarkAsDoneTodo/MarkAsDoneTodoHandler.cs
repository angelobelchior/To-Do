namespace MediatrPoC.Applications.Todos.MarkAsDoneTodo;

public record MarkAsDoneTodoRequest(Guid Id, bool IsDone) : IRequest
{
    public class MarkAsDoneTodoRequestValidator : AbstractValidator<MarkAsDoneTodoRequest>
    {
        public MarkAsDoneTodoRequestValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }
}

public record MarkAsDoneTodoNotification() : NotificationBase;

public class MarkAsDoneTodoHandler : IRequestHandler<MarkAsDoneTodoRequest>
{
    private readonly IMediator _mediator;
    private readonly ITodosRepository _repository;
    public MarkAsDoneTodoHandler(IMediator mediator, ITodosRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<Unit> Handle(MarkAsDoneTodoRequest request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetById(request.Id);
        if (todo is null) return Unit.Value; //Retornar um valor estruturado como NotFound

        var update = todo with { IsDone = request.IsDone };

        await _repository.Update(update);
        await _mediator.Publish(new MarkAsDoneTodoNotification
        {
            UniqueId = request.Id,
            Application = "Todo",
            Feature = "MarkAsDoneTodo"
        }, cancellationToken);

        return Unit.Value;
    }
}
