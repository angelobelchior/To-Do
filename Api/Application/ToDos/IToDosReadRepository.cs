namespace ToDo.Api.Application.ToDos;

public interface IToDosReadRepository
{
    Task<Contracts.ToDos.ToDo?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Contracts.ToDos.ToDo>> ListAll(bool? isDone = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<Contracts.ToDos.ToDo>> Search(string filter, bool? isDone = null, CancellationToken cancellationToken = default);
}