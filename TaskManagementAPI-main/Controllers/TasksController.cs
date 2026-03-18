using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _repository;

    public TasksController(ITaskRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BaseTask>> GetAllTasks()
    {
        var tasks = _repository.GetAll();
        return Ok(tasks);
    }

    [HttpPost("bug")]
    public ActionResult CreateBugReport([FromBody] CreateBugReportDto dto)
    {
        var task = new BugReportTask(dto.Title, dto.SeverityLevel);

        task.OnTaskCompleted += HandleTaskCompleted;

        _repository.Add(task);

        return CreatedAtAction(nameof(GetAllTasks), new { id = task.Id }, task);
    }

    [HttpPost("feature")]
    public ActionResult CreateFeatureRequest([FromBody] CreateFeatureRequestDto dto)
    {
        var task = new FeatureRequestTask(dto.Title, dto.EstimatedHours);

        task.OnTaskCompleted += HandleTaskCompleted;

        _repository.Add(task);

        return CreatedAtAction(nameof(GetAllTasks), new { id = task.Id }, task);
    }

    [HttpPut("{id:guid}/complete")]
    public ActionResult CompleteTask(Guid id)
    {
        var task = _repository.GetById(id);

        if (task is null)
            return NotFound($"Task with id {id} not found.");

        task.OnTaskCompleted += HandleTaskCompleted;
        task.CompleteTask();
        _repository.Update(task);

        return Ok(new { Message = "Task completed successfully", TaskId = task.Id });
    }

    [HttpGet("filters/high-severity-bugs")]
    public ActionResult<IEnumerable<BugReportTask>> GetHighSeverityIncompleteBugs()
    {
        var result = TaskFilterService.GetHighSeverityIncompleteBugs(_repository.GetAll());
        return Ok(result);
    }

    [HttpGet("filters/feature-hours")]
    public ActionResult<int> GetIncompleteFeatureHours()
    {
        var result = TaskFilterService.GetTotalEstimatedHoursForIncompleteFeatures(_repository.GetAll());
        return Ok(result);
    }

    private void HandleTaskCompleted(BaseTask task)
    {
        Console.WriteLine($"Task completed: {task.Title} at {DateTime.UtcNow}");
    }
}