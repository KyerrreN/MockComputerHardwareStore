namespace Shared.RequestFeatures
{
    public class GraphicsCardBenchmarkParameters : RequestParameters
    {
        // Filter
        public decimal MinFps { get; set; }
        public decimal MaxFps { get; set; } = 999.9m;

        public bool ValidFpsRange => MaxFps > MinFps;

        // Search
        public string? SearchTerm { get; set; }
    }
}
