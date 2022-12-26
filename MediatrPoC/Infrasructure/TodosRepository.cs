namespace MediatrPoC.Infrasructure;

public class TodosRepository : ITodosRepository
{
    private static readonly List<Todo> TODOS = new();

    public Task Insert(Todo todo, CancellationToken cancellationToken = default)
    {
        TODOS.Add(todo);
        return Task.CompletedTask;
    }

    public Task Update(Todo todo, CancellationToken cancellationToken = default)
    {
        Delete(todo.Id);
        TODOS.Add(todo);
        return Task.CompletedTask;
    }

    public Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        TODOS.RemoveAll(todo => todo.Id == id);
        return Task.CompletedTask;
    }

    public Task<Todo?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var todo = TODOS.FirstOrDefault(todo => todo.Id == id);
        return Task.FromResult(todo);
    }

    public Task<IEnumerable<Todo>> ListAll(bool? isDone = null, CancellationToken cancellationToken = default)
    {
        var todos = TODOS.Where(todo => isDone == null || todo.IsDone == isDone);
        return Task.FromResult(todos);
    }

    public async Task<IEnumerable<Todo>> Search(string filter, bool? isDone = null, CancellationToken cancellationToken = default)
    {
        IEnumerable<Todo> todos = TODOS;
        if (isDone is not null)
            todos = await ListAll(isDone);

        todos = TODOS.Where(t =>
            t.Title.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
            t.Description.Contains(filter, StringComparison.InvariantCultureIgnoreCase)
        );
        
        return todos;
    }
}
