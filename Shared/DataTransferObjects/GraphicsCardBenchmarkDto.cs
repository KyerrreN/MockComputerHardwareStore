namespace Shared.DataTransferObjects
{
    public record GraphicsCardBenchmarkDto
    {
        public int Id { get; init; }
        public string GraphicsCardName { get; init; }
        public string GameName { get; init; }
        public string Resolution { get; init; }
        public string Settings { get; init; }
        public decimal Fps { get; init; }
        public string TestingTool { get; set; }
    };
}
