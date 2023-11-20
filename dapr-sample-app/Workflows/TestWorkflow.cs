using Dapr.Workflow;

namespace DaprSampleApp.Workflows;

public class TestWorkflow : Workflow<UploadImageCommand, bool>
{

    public async override Task<bool> RunAsync(WorkflowContext context, UploadImageCommand input)
    {
        Console.WriteLine($"I've received data with size ${input.Data.Length}");
        return true;
    }
}
