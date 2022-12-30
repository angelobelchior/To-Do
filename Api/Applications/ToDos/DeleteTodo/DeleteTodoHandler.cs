namespace ToDo.Api.Applications.Todos.DeleteTodo;

public class DeleteToDoRequestValidator : AbstractValidator<DeleteToDoRequest>
{
    public DeleteToDoRequestValidator()
        => RuleFor(x => x.Id).NotEmpty();
}

public record DeleteToDoNotification : NotificationBase;

public class DeleteToDoHandler : IRequestHandler<DeleteToDoRequest, Contracts.IResult>
{
    private readonly IMediator _mediator;
    private readonly IToDosWriteRepository _writeRepository;
    private readonly IToDosReadRepository _readRepository;
    public DeleteToDoHandler(IMediator mediator, 
        IToDosWriteRepository writeRepository,
        IToDosReadRepository readRepository)
    {
        _mediator = mediator;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<Contracts.IResult> Handle(DeleteToDoRequest request, CancellationToken cancellationToken)
    {
        var todo = await _readRepository.GetById(request.Id, cancellationToken);
        if (todo is null) return Result.EntityNotFound("ToDo", request.Id, $"To-do {request.Id} not found");

        await _writeRepository.Delete(request.Id, cancellationToken);
        await _mediator.Publish(new DeleteToDoNotification
        {
            UniqueId = request.Id,
            Application = "ToDo",
            Feature = "DeleteTodo"
        }, cancellationToken);

        return Result.Success();
    }
}
