using ComputerHardwareStore.Presentation.ActionFilters;
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
        [HttpGet(Name = "GetGraphicsCards")]
        public async Task<IActionResult> GetGraphicsCards()
        {
            var graphicsCards = await _service.GraphicsCardService.GetAllGraphicsCardsAsync(trackChanges: false);

            return Ok(graphicsCards);
        }

        [HttpGet("{id:guid}", Name = "GraphicsCardById")]
        public async Task<IActionResult> GetGraphicsCard(Guid id)
        {
            var graphicsCard = await _service.GraphicsCardService.GetGraphicsCardAsync(id, trackChanges: false);

            return Ok(graphicsCard);
        }

        [HttpGet("collection/({ids})", Name = "GraphicsCardCollection")]
        public async Task<IActionResult> GetGraphicsCardCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var graphicsCardCollection = await _service.GraphicsCardService.GetByIdsAsync(ids, trackChanges: false);

            return Ok(graphicsCardCollection);
        }

        [HttpPost(Name = "CreateGraphicsCard")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        public async Task<IActionResult> CreateGraphicsCard([FromBody] GraphicsCardForCreationDto graphicsCard)
        {
            //if (graphicsCard is null)
            //{
            //    return BadRequest("GraphicsCardForCreationDto object is null");
            //}

            _postGraphicsCardValidator.ValidateAndThrow(graphicsCard);

            var createdGraphicsCard = await _service.GraphicsCardService.CreateGraphicsCardAsync(graphicsCard);

            return CreatedAtRoute("GraphicsCardById", new { id = createdGraphicsCard.Id }, createdGraphicsCard);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateGraphicsCardCollection([FromBody] IEnumerable<GraphicsCardForCreationDto> graphicsCardCollection)
        {
            _postGraphicsCardCollectionValidator.ValidateAndThrow(graphicsCardCollection);

            var result = await _service.GraphicsCardService.CreateGraphicsCardCollectionAsync(graphicsCardCollection);

            return CreatedAtRoute("GraphicsCardCollection", new { result.ids }, result.graphicsCards);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGraphicsCard(Guid id)
        {
            await _service.GraphicsCardService.DeleteGraphicsCardAsync(id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        public async Task<IActionResult> UpdateGraphicsCard(Guid id, [FromBody] GraphicsCardForUpdateDto graphicsCardForUpdate)
        {
            //if (graphicsCardForUpdate is null)
            //    return BadRequest("GraphicsCardForUpdateDto object is null");

            _putGraphicsCardValidator.ValidateAndThrow(graphicsCardForUpdate);

            await _service.GraphicsCardService.UpdateGraphicsCardAsync(id, graphicsCardForUpdate, trackChanges: true);

            return NoContent();
        }
        [HttpOptions]
        public IActionResult GetGraphicsCardOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");

            return Ok();
        }
    }
}
