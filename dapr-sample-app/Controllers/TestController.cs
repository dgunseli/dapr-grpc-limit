using Dapr.Client;
using DaprSampleApp.Workflows;
using Microsoft.AspNetCore.Mvc;

namespace DaprSampleApp.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{

    public TestController()
    {
    }

    [HttpPost]
    [Obsolete]
    public async Task<bool> Insert(
        [FromForm] IFormFile imageData, 
        [FromServices] DaprClient daprClient)
    {
        await using var memoryStream = new MemoryStream();
        await imageData.CopyToAsync(memoryStream);
        var data = memoryStream.ToArray();

        // Creating business request model
        UploadImageCommand createCommand = new UploadImageCommand
        {
            UserId = Guid.NewGuid(),
            Data = data
        };

        // Preparing the workflow request
        string workflowComponent = "dapr";
        string workflowName = nameof(TestWorkflow);
        string instanceId = Guid.NewGuid().ToString();

        // Calling workflow for creating the user
        var wfResponse = await daprClient.StartWorkflowAsync(
            workflowComponent: workflowComponent,
            workflowName: workflowName,
            input: createCommand,
            instanceId: instanceId
        );

        // Getting the worklflow state
        var state = await daprClient.WaitForWorkflowCompletionAsync(
                instanceId: instanceId,
                workflowComponent: workflowComponent
        );

        return true;
    }
}
