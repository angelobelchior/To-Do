namespace MediatrPoC.Applications.Todos.DeleteTodo;

public record DeleteTodoRequest(Guid Id) : IRequest
{
    public class DeleteTodoRequestValidator : AbstractValidator<DeleteTodoRequest>
    {
        public DeleteTodoRequestValidator()
            => RuleFor(x => x.Id).NotEmpty();
    }
}

public record DeleteTodoNotification() : NotificationBase;

public class DeleteTodoHandler : IRequestHandler<DeleteTodoRequest>
{
    private readonly IMediator _mediator;
    private readonly ITodosRepository _repository;
    public DeleteTodoHandler(IMediator mediator, ITodosRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTodoRequest request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Id);
        await _mediator.Publish(new DeleteTodoNotification
        {
            UniqueId = request.Id,
            Application = "Todo",
            Feature = "DeleteTodo"
        }, cancellationToken);
        
        return Unit.Value;
    }
}
