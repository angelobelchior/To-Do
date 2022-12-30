namespace ToDo.Api.Applications.Todos.UpdateTodo;

public class UpdateToDoRequestValidator : AbstractValidator<UpdateToDoRequest>
{
    public UpdateToDoRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100).MinimumLength(3);
        RuleFor(x => x.Description).MaximumLength(800).MinimumLength(3);
    }
}

public record UpdateToDoNotification : NotificationBase;

public class UpdateToDoHandler : IRequestHandler<UpdateToDoRequest, Contracts.IResult>
{
    private readonly IMediator _mediator;
    private readonly IToDosWriteRepository _writeRepository;
    private readonly IToDosReadRepository _readRepository;
    public UpdateToDoHandler(IMediator mediator, IToDosWriteRepository writeRepository,
        IToDosReadRepository readRepository)
    {
        _mediator = mediator;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<Contracts.IResult> Handle(UpdateToDoRequest request, CancellationToken cancellationToken)
    {
        var todo = await _readRepository.GetById(request.Id, cancellationToken);
        if (todo is null) return Result.EntityNotFound("ToDo", request.Id, $"To-do {request.Id} not found");

        var update = todo with { Title = request.Title, Description = request.Description, IsDone = request.IsDone };
        await _writeRepository.Update(update, cancellationToken);
        await _mediator.Publish(new UpdateToDoNotification
        {
            UniqueId = request.Id,
            Application = "ToDo",
            Feature = "UpdateTodo"
        }, cancellationToken);

        return Result.Success();
    }
}
