using MongoDB.Driver;
using TaskManager.DTOs;
using TaskManager.Models;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Swagger services

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TaskService>();

// 2. Setup MongoDB
var connectionString = builder.Configuration.GetConnectionString("MongoDb");
var mongoClient = new MongoClient(connectionString);
var database = mongoClient.GetDatabase("TaskManagerDB");
var tasksCollection = database.GetCollection<TaskItem>("Tasks");

// We register it as a Singleton so the app reuses the same connection pool
builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

var app = builder.Build();

// 3. Enable Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 4. API Endpoints


// 1. GET ALL (The one you were missing)
app.MapGet("/api/tasks", async (TaskService service) => 
    await service.GetAllTasksAsync());

// 2. GET SINGLE
app.MapGet("/api/tasks/{id}", async (TaskService service, string id) => 
{
    var task = await service.GetTaskByIdAsync(id);
    return task is not null ? Results.Ok(task) : Results.NotFound();
});

// 3. POST (Create)
app.MapPost("/api/tasks", async (TaskService service, TaskItem task) => 
{
    await service.CreateTaskAsync(task);
    return Results.Created($"/api/tasks/{task.Id}", task);
});

// 4. PUT (Update)
app.MapPut("/api/tasks/{id}", async (TaskService service, string id, TaskItem task) => 
{
    task.Id = id; // Safety check
    var success = await service.UpdateTaskAsync(id, task);
    return success ? Results.NoContent() : Results.NotFound();
});

// 5. DELETE
app.MapDelete("/api/tasks/{id}", async (TaskService service, string id) => 
{
    var success = await service.DeleteTaskAsync(id);
    return success ? Results.Ok() : Results.NotFound();
});

app.Run();