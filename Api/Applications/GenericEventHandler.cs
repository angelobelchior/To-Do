namespace ToDo.Api.Applications;

public class GenericEventHandler<TNotification> : INotificationHandler<TNotification>
    where TNotification : INotification
{
    public Task Handle(TNotification notification, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(notification);
        Console.WriteLine($"Type: {notification.GetType().Name} - Json: {json}");
        return Task.CompletedTask;
    }
}
