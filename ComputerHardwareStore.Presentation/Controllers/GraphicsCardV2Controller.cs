using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [ApiController]
    [Route("api/graphicscards")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class GraphicsCardV2Controller : ControllerBase
    {
        private readonly IServiceManager _services;

        public GraphicsCardV2Controller(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetGraphicsCards()
        {
            var graphicsCards = await _services.GraphicsCardService.GetAllGraphicsCardsAsync(trackChanges: false);

            var graphicsCardV2 = graphicsCards.Select(gc => gc.FullName + " V2");

            return Ok(graphicsCardV2);
        }
    }
}
