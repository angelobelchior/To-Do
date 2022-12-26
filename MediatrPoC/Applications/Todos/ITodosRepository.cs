namespace MediatrPoC.Applications.Todos;

public interface ITodosRepository
{
    Task Insert(Todo todo, CancellationToken cancellationToken = default);
    Task Update(Todo todo, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
    Task<Todo?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Todo>> ListAll(bool? isDone = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<Todo>> Search(string filter, bool? isDone = null, CancellationToken cancellationToken = default);
}
