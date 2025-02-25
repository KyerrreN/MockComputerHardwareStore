using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IGraphicsCardService GraphicsCardService { get; }
        IGraphicsCardBenchmarkService GraphicsCardBenchmarkService { get; }
        IBenchmarkService BenchmarkService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
