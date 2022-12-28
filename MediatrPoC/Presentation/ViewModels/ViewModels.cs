namespace MediatrPoC.Presentation.ViewModels;

/// <summary>
/// To-do View Model
/// </summary>
/// <param name="Title">Title</param>
/// <param name="Description">Description</param>
/// <param name="IsDone">Is Done</param>
public record TodoViewModel(string Title, string Description, bool IsDone = false);

/// <summary>
/// Mark as Done To-do View Model
/// </summary>
/// <param name="IsDone">Is Done</param>
public record MarkAsDoneTodoViewModel(bool IsDone);