namespace ToDo.Api.Application.ToDos;

public interface IToDosWriteRepository
{
    Task Insert(Contracts.ToDos.ToDo todo, CancellationToken cancellationToken = default);
    Task Update(Contracts.ToDos.ToDo todo, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}
