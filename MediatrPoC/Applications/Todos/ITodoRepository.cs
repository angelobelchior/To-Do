namespace MediatrPoC.Applications.Todos;

public interface ITodoRepository
{
    Task<Todo> AddNewTodo(string title, string description, bool isDone);
    Task<IEnumerable<Todo>> ListAll();
}
