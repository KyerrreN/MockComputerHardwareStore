using Entities.Enum;

namespace Shared.DataTransferObjects
{
    public record BenchmarkForCreationDto(string GameName, BenchmarkSettings Settings, BenchmarkResolution Resolution);
}
