using MediatrPoC.Applications.Todos;

namespace MediatrPoC.Infrasructure;

public class TodoRepository : ITodoRepository
{
    private static List<Todo> TODOS = new();   

    public Task<Todo> AddNewTodo(string title, string description, bool isDone)
    {
        var todo = new Todo(Guid.NewGuid(), title, description, isDone);
        TODOS.Add(todo);
        return Task.FromResult(todo);
    }
    
    public Task<IEnumerable<Todo>> ListAll()
    {
        return Task.FromResult(TODOS.Select(t => t));
    }
}
