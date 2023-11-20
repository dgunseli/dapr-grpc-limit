using Dapr.Workflow;
using DaprSampleApp.Workflows;
using Grpc.Net.Client;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDaprWorkflow(
            options => {
                options.RegisterWorkflow<TestWorkflow>();
            }
        );
        services.AddDaprClient();
        services.AddControllers().AddDapr(
            builder => builder.UseGrpcChannelOptions(new GrpcChannelOptions()
            {

                MaxReceiveMessageSize = 30 * 1024 * 1024,
                MaxSendMessageSize = 30 * 1024 * 1024
            }));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}