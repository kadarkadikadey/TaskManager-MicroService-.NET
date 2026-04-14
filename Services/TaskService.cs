using MongoDB.Driver;
using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService
{
    private readonly IMongoCollection<TaskItem> _tasks;

    public TaskService(IMongoClient client)
    {
        var database = client.GetDatabase("TaskManagerDB");
        _tasks = database.GetCollection<TaskItem>("Tasks");
    }

    public async Task<List<TaskItem>> GetAllTasksAsync() =>
        await _tasks.Find(_ => true).ToListAsync();

    // FIXED: Added basic validation or check for null
    public async Task<TaskItem?> GetTaskByIdAsync(string id) =>
        await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();

    public async Task CreateTaskAsync(TaskItem newTask)
{
    newTask.Id = null; // Correct

    if (string.IsNullOrEmpty(newTask.Title))
        throw new Exception("Title is required");

    // FIX: Set the creation time, not just the due date
    newTask.CreatedAt = DateTime.UtcNow; 
    
    // Optional: Only set DueDate if the user didn't provide one
    if (newTask.DueDate == null) {
        newTask.DueDate = DateTime.UtcNow.AddDays(7); // Default to 7 days from now
    }

    await _tasks.InsertOneAsync(newTask);
}

    // FIXED: Ensure the ID inside the object matches the ID in the URL
    public async Task<bool> UpdateTaskAsync(string id, TaskItem updatedTask)
    {
        updatedTask.Id = id; // IMPORTANT: Force the object to use the correct ID
        var result = await _tasks.ReplaceOneAsync(t => t.Id == id, updatedTask);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> UpdateStatusAsync(string id, string newStatus)
    {
        var update = Builders<TaskItem>.Update.Set(t => t.Status, newStatus);
        var result = await _tasks.UpdateOneAsync(t => t.Id == id, update);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteTaskAsync(string id)
    {
        var result = await _tasks.DeleteOneAsync(t => t.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}