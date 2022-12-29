namespace ToDo.Contracts.ToDos;

/// <summary>
/// To-Do
/// </summary>
/// <param name="Id">Id</param>
/// <param name="Title">Title</param>
/// <param name="Description">Description</param>
/// <param name="IsDone">Is Done</param>
public record ToDo(Guid Id, string Title, string Description, bool IsDone);