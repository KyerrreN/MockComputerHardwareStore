using ComputerHardwareStore.Presentation.ActionFilters;
using ComputerHardwareStore.Presentation.ModelBinders;
using FluentValidation;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [Route("api/graphicscards")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
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
        /// <summary>
        /// Gets the list of all graphics cards
        /// </summary>
        /// <returns>Graphics Cards list</returns>
        /// <response code="200">Returns list of all Graphics Cards</response>
        [HttpGet(Name = "GetGraphicsCards")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetGraphicsCards()
        {
            var graphicsCards = await _service.GraphicsCardService.GetAllGraphicsCardsAsync(trackChanges: false);

            return Ok(graphicsCards);
        }

        /// <summary>
        /// Gets one entry of a graphics card by a specified id
        /// </summary>
        /// <param name="id">Id of a graphics card</param>
        /// <returns>Graphics card item</returns>
        /// <response code="200">Returns found graphics card item</response>
        /// <response code="404">If graphics card with specified id doesn't exist</response>
        [HttpGet("{id:guid}", Name = "GraphicsCardById")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGraphicsCard(Guid id)
        {
            var graphicsCard = await _service.GraphicsCardService.GetGraphicsCardAsync(id, trackChanges: false);

            return Ok(graphicsCard);
        }

        /// <summary>
        /// Gets all entries of graphics cards with specified Ids
        /// </summary>
        /// <param name="ids">Graphics Cards Id collection</param>
        /// <returns>List of found graphics cards</returns>
        /// <response code="200">Returns list of found graphics cards</response>
        /// <response code="400">If ids collection is null</response>
        /// <response code="422">If ids collection is invalid, or there is a mismatch between ids collection length and found graphics cards</response>
        [HttpGet("collection/({ids})", Name = "GraphicsCardCollection")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGraphicsCardCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var graphicsCardCollection = await _service.GraphicsCardService.GetByIdsAsync(ids, trackChanges: false);

            return Ok(graphicsCardCollection);
        }

        /// <summary>
        /// Creates a graphics card entry
        /// </summary>
        /// <param name="graphicsCard"></param>
        /// <returns>A newly created graphics card</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If an item is null</response>
        /// <response code="401">If no Beared token is provided, or user does not have sufficient rights</response>
        /// <response code="422">If validation on the model fails</response>
        [HttpPost(Name = "CreateGraphicsCard")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateGraphicsCard([FromBody] GraphicsCardForCreationDto graphicsCard)
        {
            _postGraphicsCardValidator.ValidateAndThrow(graphicsCard);

            var createdGraphicsCard = await _service.GraphicsCardService.CreateGraphicsCardAsync(graphicsCard);

            return CreatedAtRoute("GraphicsCardById", new { id = createdGraphicsCard.Id }, createdGraphicsCard);
        }

        /// <summary>
        /// Creates several graphics card entires
        /// </summary>
        /// <param name="graphicsCardCollection"></param>
        /// <returns>Newly created collection of graphics cards</returns>
        /// <response code="201">Returns the newlt created items</response>
        /// <response code="400">If a collection of graphics cards is null</response>
        /// <response code="401">If Bearer token is not provided, or user does not have sufficient rights</response>
        /// <response code="422">If a collection of graphics cards is invalid</response>
        [HttpPost("collection")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateGraphicsCardCollection([FromBody] IEnumerable<GraphicsCardForCreationDto> graphicsCardCollection)
        {
            _postGraphicsCardCollectionValidator.ValidateAndThrow(graphicsCardCollection);

            var result = await _service.GraphicsCardService.CreateGraphicsCardCollectionAsync(graphicsCardCollection);

            return CreatedAtRoute("GraphicsCardCollection", new { result.ids }, result.graphicsCards);
        }

        /// <summary>
        /// Deletes a graphics card item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">If a graphics card was deleted succesfully</response>
        /// <response code="401">If no Bearer token is provided, or user does not have sufficient rights</response>
        /// <response code="404">If a graphics card with specified GUID does not exist</response>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteGraphicsCard(Guid id)
        {
            await _service.GraphicsCardService.DeleteGraphicsCardAsync(id, trackChanges: false);

            return NoContent();
        }

        /// <summary>
        /// Updates a graphics card item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="graphicsCardForUpdate"></param>
        /// <returns></returns>
        /// <response code="204">If a graphics card was updated succesfully</response>
        /// <response code="400">If a graphics card dto is null</response>
        /// <response code="401">If a Bearer token is not provided, or user does not have sufficient rights</response>
        /// <response code="404">If a graphics card with specified GUID does not exist</response>
        /// <response code="422">If a graphics card dto is invalid</response>
        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(BindingValidationFilterAttribute))]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> UpdateGraphicsCard(Guid id, [FromBody] GraphicsCardForUpdateDto graphicsCardForUpdate)
        {
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
