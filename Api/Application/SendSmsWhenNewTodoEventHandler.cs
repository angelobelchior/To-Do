using ToDo.Api.Application.ToDos;
using ToDo.Api.Application.ToDos.AddNewTodo;

namespace ToDo.Api.Application;

public class SendSmsWhenNewTodoEventHandler : INotificationHandler<AddNewToDoNotification>
{
    private readonly IToDosReadRepository _readRepository;
    public SendSmsWhenNewTodoEventHandler(IToDosReadRepository readRepository)
    {
        _readRepository = readRepository;
    }
    
    public async Task Handle(AddNewToDoNotification notification, CancellationToken cancellationToken)
    {
        var todo = await _readRepository.GetById(notification.UniqueId, cancellationToken);
        
        Console.WriteLine($"Sending SMS. Title {todo?.Title}... ");
    }
}