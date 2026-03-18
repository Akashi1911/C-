namespace TaskManagementAPI.Models;

public class FeatureRequestTask : BaseTask
{
    public int EstimatedHours { get; private set; }

    public FeatureRequestTask(string title, int estimatedHours) : base(title)
    {
        EstimatedHours = estimatedHours;
    }
}