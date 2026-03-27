using KaraHan.Application.Abstractions;
using KaraHan.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KaraHan.API.Controllers;

[Authorize]
[Route("api/tasks")]
public class TasksController : BaseApiController
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskDto>>> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await _taskService.GetTasksAsync(UserId, cancellationToken);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> Create([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var createdTask = await _taskService.CreateTaskAsync(UserId, request, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{taskId:int}")]
    public async Task<ActionResult<TaskDto>> Update(int taskId, [FromBody] UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var updatedTask = await _taskService.UpdateTaskAsync(UserId, taskId, request, cancellationToken);
        return updatedTask is null ? NotFound() : Ok(updatedTask);
    }

    [HttpDelete("{taskId:int}")]
    public async Task<IActionResult> Delete(int taskId, CancellationToken cancellationToken)
    {
        var isDeleted = await _taskService.DeleteTaskAsync(UserId, taskId, cancellationToken);
        return isDeleted ? NoContent() : NotFound();
    }
}
