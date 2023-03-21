using ToDo.Api.Application.ToDos;

namespace ToDo.Api.Infrastructure;

public class ToDosRepository : IToDosWriteRepository, IToDosReadRepository
{
    private static readonly List<Contracts.ToDos.ToDo> Todos = new();

    public Task Insert(Contracts.ToDos.ToDo todo, CancellationToken cancellationToken = default)
    {
        Todos.Add(todo);
        return Task.CompletedTask;
    }

    public Task Update(Contracts.ToDos.ToDo todo, CancellationToken cancellationToken = default)
    {
        Delete(todo.Id, cancellationToken);
        Todos.Add(todo);
        return Task.CompletedTask;
    }

    public Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        Todos.RemoveAll(todo => todo.Id == id);
        return Task.CompletedTask;
    }

    public Task<Contracts.ToDos.ToDo?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var todo = Todos.FirstOrDefault(todo => todo.Id == id);
        return Task.FromResult(todo);
    }

    public Task<IEnumerable<Contracts.ToDos.ToDo>> ListAll(bool? isDone = null, CancellationToken cancellationToken = default)
    {
        var todos = Todos.Where(todo => isDone == null || todo.IsDone == isDone);
        return Task.FromResult(todos);
    }

    public async Task<IEnumerable<Contracts.ToDos.ToDo>> Search(string filter, bool? isDone = null, CancellationToken cancellationToken = default)
    {
        IEnumerable<Contracts.ToDos.ToDo> todos;
        if (isDone is not null)
            todos = await ListAll(isDone, cancellationToken);

        todos = Todos.Where(t =>
            t.Title.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            t.Description.Contains(filter, StringComparison.InvariantCultureIgnoreCase)
        );

        return todos;
    }
}
