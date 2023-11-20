namespace DaprSampleApp.Workflows
{
    public record class UploadImageCommand
    {
        public Guid UserId  {get; init; }
        public required byte[] Data  {get; init; }
    }
}