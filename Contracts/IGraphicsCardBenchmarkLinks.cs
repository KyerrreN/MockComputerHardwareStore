using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Contracts
{
    public interface IGraphicsCardBenchmarkLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<GraphicsCardBenchmarkDto> graphicsCardBenchmarkDto,
                                      string fields, Guid graphicsCardId, HttpContext httpContext);
    }
}
