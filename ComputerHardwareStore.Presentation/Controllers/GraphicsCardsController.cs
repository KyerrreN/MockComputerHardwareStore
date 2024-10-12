using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [Route("api/graphicscards")]
    //[ApiController]
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

        [HttpGet("{id:guid}", Name = "GraphicsCardById")]
        public IActionResult GetGraphicsCard(Guid id)
        {
            var graphicsCard = _service.GraphicsCardService.GetGraphicsCard(id, trackChanges: false);

            return Ok(graphicsCard);
        }

        [HttpGet("collection/({ids})", Name = "GraphicsCardCollection")]
        public IActionResult GetGraphicsCardCollection(IEnumerable<Guid> ids)
        {
            var graphicsCardCollection = _service.GraphicsCardService.GetByIds(ids, trackChanges: false);

            return Ok(graphicsCardCollection);
        }

        [HttpPost]
        public IActionResult CreateGraphicsCard([FromBody] GraphicsCardForCreationDto graphicsCard)
        {
            if (graphicsCard is null)
            {
                return BadRequest("GraphicsCardForCreationDto object is null");
            }

            var createdGraphicsCard = _service.GraphicsCardService.CreateGraphicsCard(graphicsCard);

            return CreatedAtRoute("GraphicsCardById", new { id = createdGraphicsCard.Id }, createdGraphicsCard);
        }
    }
}
