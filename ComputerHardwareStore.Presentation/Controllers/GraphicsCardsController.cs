using ComputerHardwareStore.Presentation.ModelBinders;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [Route("api/graphicscards")]
    [ApiController]
    public class GraphicsCardsController : ControllerBase
    {
        // DI
        private readonly IServiceManager _service;
        private readonly IValidator<GraphicsCardForCreationDto> _postGraphicsCardValidator;
        private readonly IValidator<GraphicsCardForUpdateDto> _putGraphicsCardValidator;
        private readonly IValidator<IEnumerable<GraphicsCardForCreationDto>> _postGraphicsCardCollectionValidator;

        public GraphicsCardsController(IServiceManager service,
                                       IValidator<GraphicsCardForCreationDto> postGraphicsCardValidator,
                                       IValidator<GraphicsCardForUpdateDto> putGraphicsCardValidator,
                                       IValidator<IEnumerable<GraphicsCardForCreationDto>> postGraphicsCardCollectionValidator)
        {
            _service = service;
            _postGraphicsCardValidator = postGraphicsCardValidator;
            _putGraphicsCardValidator = putGraphicsCardValidator;
            _postGraphicsCardCollectionValidator = postGraphicsCardCollectionValidator;
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
        public IActionResult GetGraphicsCardCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
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

            _postGraphicsCardValidator.ValidateAndThrow(graphicsCard);

            var createdGraphicsCard = _service.GraphicsCardService.CreateGraphicsCard(graphicsCard);

            return CreatedAtRoute("GraphicsCardById", new { id = createdGraphicsCard.Id }, createdGraphicsCard);
        }

        [HttpPost("collection")]
        public IActionResult CreateGraphicsCardCollection([FromBody] IEnumerable<GraphicsCardForCreationDto> graphicsCardCollection)
        {
            _postGraphicsCardCollectionValidator.ValidateAndThrow(graphicsCardCollection);

            var result = _service.GraphicsCardService.CreateGraphicsCardCollection(graphicsCardCollection);

            return CreatedAtRoute("GraphicsCardCollection", new { result.ids }, result.graphicsCards);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteGraphicsCard(Guid id)
        {
            _service.GraphicsCardService.DeleteGraphicsCard(id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateGraphicsCard(Guid id, [FromBody] GraphicsCardForUpdateDto graphicsCardForUpdate)
        {
            if (graphicsCardForUpdate is null)
                return BadRequest("GraphicsCardForUpdateDto object is null");

            _putGraphicsCardValidator.ValidateAndThrow(graphicsCardForUpdate);

            _service.GraphicsCardService.UpdateGraphicsCard(id, graphicsCardForUpdate, trackChanges: true);

            return NoContent();
        }
    }
}
