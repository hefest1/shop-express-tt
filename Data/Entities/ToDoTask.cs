namespace Data.Entities;

public class ToDoTask : Entity
{
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsCompleted { get; set; }

    public ToDoTask()
    {
        Title = string.Empty;
    }
}