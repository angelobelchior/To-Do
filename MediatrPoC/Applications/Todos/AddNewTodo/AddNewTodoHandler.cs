using FluentValidation;

using MediatR;
using MediatrPoC.Applications.Todos;

namespace MediatrPoC.Applications.Todos.AddNewTodo;

public record AddNewTodoRequest(string Title, string Description, bool IsDone = false) : IRequest<AddNewTodoResponse>
{
    public class AddNewTodoRequestValidator : AbstractValidator<AddNewTodoRequest>
    {
        public AddNewTodoRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}

public record AddNewTodoResponse(Todo Todo);

public record AddNewTodoNotification(Guid UniqueId, string Application, string Feature) : INotification
{
    public DateTime DateTime => DateTime.Now;
}

public class AddNewTodoHandler : IRequestHandler<AddNewTodoRequest, AddNewTodoResponse>
{
    private readonly IMediator _mediator;
    private readonly ITodoRepository _addNewTodoRepository;
    public AddNewTodoHandler(IMediator mediator, ITodoRepository addNewTodoRepository)
    {
        _mediator = mediator;
        _addNewTodoRepository = addNewTodoRepository;
    }

    public async Task<AddNewTodoResponse> Handle(AddNewTodoRequest request, CancellationToken cancellationToken)
    {
        var todo = await _addNewTodoRepository.AddNewTodo(request.Title, request.Description, request.IsDone);
        await _mediator.Publish(new AddNewTodoNotification(todo.Id, "Todo", "AddNewTodo"), cancellationToken);
        return new AddNewTodoResponse(todo);
    }
}