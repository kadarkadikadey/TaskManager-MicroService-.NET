namespace TaskManager.DTOs;

public class TaskResponse
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Status { get; set; } = null!;
}