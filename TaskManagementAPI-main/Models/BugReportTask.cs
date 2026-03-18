namespace TaskManagementAPI.Models;

public class BugReportTask : BaseTask
{
    public string SeverityLevel { get; private set; }

    public BugReportTask(string title, string severityLevel) : base(title)
    {
        SeverityLevel = severityLevel;
    }
}