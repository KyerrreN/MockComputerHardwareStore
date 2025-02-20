using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;
using Microsoft.Net.Http.Headers;
using Entities.LinkFolder;

namespace ComputerHardwareStore.Utility
{
    public class GraphicsCardBenchmarkLinks : IGraphicsCardBenchmarkLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<GraphicsCardBenchmarkDto> _dataShaper;

        public GraphicsCardBenchmarkLinks(LinkGenerator linkGenerator, 
                                          IDataShaper<GraphicsCardBenchmarkDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<GraphicsCardBenchmarkDto> graphicsCardBenchmarkDto,
                                             string fields, Guid graphicsCardId, HttpContext httpContext)
        {
            var shapedGraphicsCardBenchmark = ShapeData(graphicsCardBenchmarkDto, fields);

            if (ShouldGenerateLinks(httpContext))
            {
                return ReturnLinkedGraphicsCardBenchmarks(graphicsCardBenchmarkDto, fields, graphicsCardId, httpContext, shapedGraphicsCardBenchmark);
            }

            return ReturnShapedGraphicsCardBenchmarks(shapedGraphicsCardBenchmark);
        }

        // private
        private List<Entity> ShapeData(IEnumerable<GraphicsCardBenchmarkDto> graphicsCardBenchmarkDto, string fields)
        {
            return _dataShaper.ShapeData(graphicsCardBenchmarkDto, fields)
                .Select(e => e.Entity)
                .ToList();
        }
        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
        private LinkResponse ReturnShapedGraphicsCardBenchmarks(List<Entity> shapedGraphicsCardBenchmarks)
        {
            return new LinkResponse
            {
                ShapedEntities = shapedGraphicsCardBenchmarks
            };
        }
        private LinkResponse ReturnLinkedGraphicsCardBenchmarks(IEnumerable<GraphicsCardBenchmarkDto> graphicsCardBenchmarkDto,
                                                                string fields, Guid graphicsCardId, HttpContext httpContext,
                                                                List<Entity> shapedGraphicsCardBenchmarks)
        {
            var graphicsCardBenchmarkDtoList = graphicsCardBenchmarkDto.ToList();

            for (var index = 0; index < graphicsCardBenchmarkDtoList.Count(); index++)
            {
                var graphicsCardBenchmarkLinks = CreateLinksForGraphicsCardBenchmark(httpContext, graphicsCardId, graphicsCardBenchmarkDtoList[index].Id, fields);
                shapedGraphicsCardBenchmarks[index].Add("Links", graphicsCardBenchmarkLinks);
            }

            var graphicCardBenchmarkCollection = new LinkCollectionWrapper<Entity>(shapedGraphicsCardBenchmarks);
            var linkedGraphicsCardBenchmarks = CreateLinksForGraphicsCardBenchmarks(httpContext, graphicCardBenchmarkCollection);

            return new LinkResponse
            {
                HasLinks = true,
                LinkedEntities = linkedGraphicsCardBenchmarks
            };
        }

        private List<Link> CreateLinksForGraphicsCardBenchmark(HttpContext httpContext, Guid graphicsCardId, Guid id, string fields = "")
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(httpContext, "GetGraphicsCardBenchmark", values: new { graphicsCardId, id, fields }),
                         "self",
                         "GET"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteGraphicsCardBenchmark", values: new { graphicsCardId, id }),
                         "delete_graphics_card_benchmark",
                         "DELETE"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateGraphicsCardBenchmark", values: new { graphicsCardId, id }),
                         "update_graphics_card_benchmark",
                         "PUT")
            };

            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForGraphicsCardBenchmarks(HttpContext httpContext, LinkCollectionWrapper<Entity> graphicsCardBenchmarkWrapper)
        {
            graphicsCardBenchmarkWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetGraphicsCardBenchmarks", values: new { }),
                                         "self",
                                         "GET"));

            return graphicsCardBenchmarkWrapper;
        }
    }
}
