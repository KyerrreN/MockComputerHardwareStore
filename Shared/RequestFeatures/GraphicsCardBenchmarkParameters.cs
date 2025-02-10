using Entities.Exceptions;

namespace Shared.RequestFeatures
{
    public class GraphicsCardBenchmarkParameters : RequestParameters
    {
        public decimal MinFps { get; set; }
        public decimal MaxFps { get; set; }

        public bool ValidFpsRange => MaxFps > MinFps;
    }
}
