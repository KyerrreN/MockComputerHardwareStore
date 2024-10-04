using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class GraphicsCardConfiguration : IEntityTypeConfiguration<GraphicsCard>
    {
        public void Configure(EntityTypeBuilder<GraphicsCard> builder)
        {
            builder.HasData
            (
                new GraphicsCard
                {
                    Id = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                    Manufacturer = "NVIDIA",
                    Model = "RTX 2060 Super",
                    Price = 262.17M,
                    StockQuantity = 17,
                    CategoryId = new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"),
                    Distributor = "MSI",
                    BaseClockSpeed = "1470",
                    MaxClockSpeed = "1650",
                    MemoryClockSpeed = "14000",
                    ConnectorPins = 8,
                    IsSupportRtx = true
                },
                new GraphicsCard
                {
                    Id = new Guid("5280896c-12eb-49fc-a011-1bee7b7032f0"),
                    Manufacturer = "AMD",
                    Model = "RX 580",
                    Price = 80.39M,
                    StockQuantity = 3,
                    CategoryId = new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"),
                    Distributor = "Sapphire",
                    BaseClockSpeed = "1210",
                    MaxClockSpeed = "1411",
                    MemoryClockSpeed = "8000",
                    ConnectorPins = 14,
                    IsSupportRtx = false
                },
                new GraphicsCard
                {
                    Id = new Guid("50ea126c-b789-4a14-bb74-153ef1cb018b"),
                    Manufacturer = "NVIDIA",
                    Model = "RTX 4080 Super",
                    Price = 1139.99M,
                    StockQuantity = 21,
                    CategoryId = new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"),
                    Distributor = "MSI",
                    BaseClockSpeed = "2295",
                    MaxClockSpeed = "2595",
                    MemoryClockSpeed = "23000",
                    ConnectorPins = 16,
                    IsSupportRtx = true
                }
            );
        }
    }
}
