namespace MediatrPoC.Infrasructure;

public class TodosRepository : ITodosRepository
{
    private static readonly List<Todo> Todos = new();

    public Task Insert(Todo todo, CancellationToken cancellationToken = default)
    {
        Todos.Add(todo);
        return Task.CompletedTask;
    }

    public Task Update(Todo todo, CancellationToken cancellationToken = default)
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

    public Task<Todo?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var todo = Todos.FirstOrDefault(todo => todo.Id == id);
        return Task.FromResult(todo);
    }

    public Task<IEnumerable<Todo>> ListAll(bool? isDone = null, CancellationToken cancellationToken = default)
    {
        var todos = Todos.Where(todo => isDone == null || todo.IsDone == isDone);
        return Task.FromResult(todos);
    }

    public async Task<IEnumerable<Todo>> Search(string filter, bool? isDone = null, CancellationToken cancellationToken = default)
    {
        IEnumerable<Todo> todos;
        if (isDone is not null)
            todos = await ListAll(isDone, cancellationToken);

        todos = Todos.Where(t =>
            t.Title.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            t.Description.Contains(filter, StringComparison.InvariantCultureIgnoreCase)
        );

        return todos;
    }
}
