namespace ToDo.Api.Application.ToDos.AddNewTodo;

public class AddNewToDoRequestValidator : AbstractValidator<AddNewToDoRequest>
{
    public AddNewToDoRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100).MinimumLength(3);
        RuleFor(x => x.Description).MaximumLength(800).MinimumLength(3);
    }
}

public record AddNewToDoNotification : NotificationBase;

public class AddNewToDoHandler : IRequestHandler<AddNewToDoRequest, IResult<AddNewToDoResponse>>
{
    private readonly IMediator _mediator;
    private readonly IToDosWriteRepository _repository;
    public AddNewToDoHandler(IMediator mediator, IToDosWriteRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<IResult<AddNewToDoResponse>> Handle(AddNewToDoRequest request, CancellationToken cancellationToken)
    {
        var todo = request.ToModel();
        await _repository.Insert(todo, cancellationToken);
        await _mediator.Publish(new AddNewToDoNotification
        {
            UniqueId = todo.Id,
            Application = "ToDo",
            Feature = "AddNewTodo"
        }, cancellationToken);

        return Result<AddNewToDoResponse>.Success(new AddNewToDoResponse(todo));
    }
}