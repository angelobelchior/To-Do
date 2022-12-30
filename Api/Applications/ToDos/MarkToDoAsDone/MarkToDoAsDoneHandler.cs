namespace ToDo.Api.Applications.Todos.MarkAsDoneTodo;

public class MarkToDoAsDoneRequestValidator : AbstractValidator<MarkToDoAsDoneRequest>
{
    public MarkToDoAsDoneRequestValidator()
        => RuleFor(x => x.Id).NotEmpty();
}

public record MarkTodoAsDoneNotification : NotificationBase;

public class MarkToDoAsDoneHandler : IRequestHandler<MarkToDoAsDoneRequest, Contracts.IResult>
{
    private readonly IMediator _mediator;
    private readonly IToDosWriteRepository _writeRepository;
    private readonly IToDosReadRepository _readRepository;
    public MarkToDoAsDoneHandler(IMediator mediator, 
        IToDosWriteRepository writeRepository, 
        IToDosReadRepository readRepository)
    {
        _mediator = mediator;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<Contracts.IResult> Handle(MarkToDoAsDoneRequest request, CancellationToken cancellationToken)
    {
        var todo = await _readRepository.GetById(request.Id, cancellationToken);
        if (todo is null) return Result.EntityNotFound("ToDo", request.Id, $"To-do {request.Id} not found");

        var update = todo with { IsDone = request.IsDone };

        await _writeRepository.Update(update, cancellationToken);
        await _mediator.Publish(new MarkTodoAsDoneNotification
        {
            UniqueId = request.Id,
            Application = "ToDo",
            Feature = "MarkAsDoneTodo"
        }, cancellationToken);

        return Result.Success();
    }
}
