using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ComputerHardwareStore.Presentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType.Contains("application/vnd.kyerrren.apiroot"))
            {
                var list = new List<Link>
                {
                    new Link(_linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new { }),
                             "self",
                             "GET"),
                    new Link(_linkGenerator.GetUriByName(HttpContext, "GetGraphicsCards", new { }),
                             "graphics_cards",
                             "GET"),
                    new Link(_linkGenerator.GetUriByName(HttpContext, "CreateGraphicsCard", new { }),
                             "create_graphics_card",
                             "POST"),
                    new Link(_linkGenerator.GetUriByName(HttpContext, "GetAllBenchmarks", new { }),
                             "benchmarks",
                             "GET"),
                    new Link(_linkGenerator.GetUriByName(HttpContext, "CreateBenchmark", new { }),
                             "create_benchmark",
                             "POST")
                };

                return Ok(list);
            }

            return NoContent();
        }
    }
}
