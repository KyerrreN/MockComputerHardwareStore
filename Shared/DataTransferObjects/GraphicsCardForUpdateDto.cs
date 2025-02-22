namespace Shared.DataTransferObjects
{
    public record GraphicsCardForUpdateDto
    {
        public string Distributor { get; init; }
        public string Manufacturer { get; init; }
        public string Model { get; init; }
        public string BaseClockSpeed { get; init; }
        public string MaxClockSpeed { get; init; }
        public string MemoryClockSpeed { get; init; }
        public byte ConnectorPins { get; init; }
        public bool IsSupportRtx { get; init; }
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public IEnumerable<GraphicsCardBenchmarkForCreationDto> GraphicsCardBenchmarks { get; init; }
    }
}
