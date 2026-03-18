using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories;

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<BaseTask> _tasks = new();

    public InMemoryTaskRepository()
    {
        _tasks.Add(new BugReportTask("Login button not working", "High"));
        _tasks.Add(new BugReportTask("Typos on homepage", "Low"));
        _tasks.Add(new FeatureRequestTask("Add dark mode", 8));
        _tasks.Add(new FeatureRequestTask("Export to PDF", 12));
    }

    public IEnumerable<BaseTask> GetAll() => _tasks;

    public BaseTask? GetById(Guid id) => _tasks.FirstOrDefault(t => t.Id == id);

    public void Add(BaseTask task)
    {
        _tasks.Add(task);
    }

    public void Update(BaseTask task)
    {
        // In-memory list updates object by reference,
        // so nothing special is needed here.
    }
}