﻿using ComputerHardwareStore.Presentation.ModelBinders;
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

            var createdGraphicsCard = _service.GraphicsCardService.CreateGraphicsCard(graphicsCard);

            return CreatedAtRoute("GraphicsCardById", new { id = createdGraphicsCard.Id }, createdGraphicsCard);
        }

        [HttpPost("collection")]
        public IActionResult CreateGraphicsCardCollection([FromBody] IEnumerable<GraphicsCardForCreationDto> graphicsCardCollection)
        {
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

            _service.GraphicsCardService.UpdateGraphicsCard(id, graphicsCardForUpdate, trackChanges: true);

            return NoContent();
        }
    }
}
