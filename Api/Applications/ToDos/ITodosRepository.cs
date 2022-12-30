namespace ToDo.Api.Applications.Todos;

public interface IToDosWriteRepository
{
    Task Insert(Contracts.ToDos.ToDo todo, CancellationToken cancellationToken = default);
    Task Update(Contracts.ToDos.ToDo todo, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}

public interface IToDosReadRepository
{
    Task<Contracts.ToDos.ToDo?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Contracts.ToDos.ToDo>> ListAll(bool? isDone = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<Contracts.ToDos.ToDo>> Search(string filter, bool? isDone = null, CancellationToken cancellationToken = default);
}
