﻿using System.Runtime.CompilerServices;

namespace MediatrPoC.Applications.Todos.AddNewTodo;

public record AddNewTodoRequest(string Title, string Description, bool IsDone) : IRequest<AddNewTodoResponse>
{
    public Todo ToModel()
        => new(Guid.NewGuid(), Title, Description, IsDone);

    public class AddNewTodoRequestValidator : AbstractValidator<AddNewTodoRequest>
    {
        public AddNewTodoRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100).MinimumLength(3);
            RuleFor(x => x.Description).MaximumLength(800).MinimumLength(3);
        }
    }
}

public record AddNewTodoResponse(Todo Todo);

public record AddNewTodoNotification() : NotificationBase;

public class AddNewTodoHandler : IRequestHandler<AddNewTodoRequest, AddNewTodoResponse>
{
    private readonly IMediator _mediator;
    private readonly ITodosRepository _repository;
    public AddNewTodoHandler(IMediator mediator, ITodosRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<AddNewTodoResponse> Handle(AddNewTodoRequest request, CancellationToken cancellationToken)
    {
        var todo = request.ToModel();
        await _repository.Insert(todo);
        await _mediator.Publish(new AddNewTodoNotification
        {
            UniqueId = todo.Id,
            Application = "Todo",
            Feature = "AddNewTodo"
        }, cancellationToken);
        return new AddNewTodoResponse(todo);
    }
}