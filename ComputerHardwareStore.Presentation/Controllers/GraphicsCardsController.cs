using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [Route("api/graphicscards")]
    [ApiController]
    public class GraphicsCardsController : ControllerBase
    {
        // DI
        private readonly IServiceManager _service;

        public GraphicsCardsController(IServiceManager service)
        {
            _service = service;
        }

        // Action Methods
        [HttpGet]
        public IActionResult GetGraphicsCards()
        {
            var graphicsCards = _service.GraphicsCardService.GetAllGraphicsCards(trackChanges: false);

            return Ok(graphicsCards);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetGraphicsCard(Guid id)
        {
            var graphicsCard = _service.GraphicsCardService.GetGraphicsCard(id, trackChanges: false);
            return Ok(graphicsCard);
        }
    }
}
