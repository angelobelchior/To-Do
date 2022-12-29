namespace MediatrPoC.Applications;

public abstract record NotificationBase : INotification
{
    public required Guid UniqueId { get; init; }
    public required string Application { get; init; }
    public required string Feature { get; init; }
    public DateTime DateTime => DateTime.Now;
}
