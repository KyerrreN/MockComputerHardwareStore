﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // DI
        private readonly IServiceManager _service;

        public ProductsController(IServiceManager service)
        {
            _service = service;
        }

        // Action Methods
        [HttpGet]
        public IActionResult GetGraphicsCards()
        {
            try
            {
                var graphicsCards = _service.GraphicsCardService.GetAllGraphicsCards(trackChanges: false);

                return Ok(graphicsCards);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}