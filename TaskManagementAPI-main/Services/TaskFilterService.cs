using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services;

public static class TaskFilterService
{
    public static List<BugReportTask> GetHighSeverityIncompleteBugs(IEnumerable<BaseTask> tasks)
    {
        return tasks
            .OfType<BugReportTask>()
            .Where(t => !t.IsCompleted && t.SeverityLevel.Equals("High", StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(t => t.CreatedAt)
            .ToList();
    }

    public static int GetTotalEstimatedHoursForIncompleteFeatures(IEnumerable<BaseTask> tasks)
    {
        return tasks
            .OfType<FeatureRequestTask>()
            .Where(t => !t.IsCompleted)
            .Sum(t => t.EstimatedHours);
    }
}