using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record BenchmarkDto
    {
        public Guid Id { get; init; }
        public string GameName { get; init; }
        public string Settings { get; init; }
        public string Resolution { get; init; }
    }
}
