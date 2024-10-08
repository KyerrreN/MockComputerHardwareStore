using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;
using System.Text;

namespace ComputerHardwareStore
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(GraphicsCardDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<GraphicsCardDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        private static void FormatCsv(StringBuilder buffer, GraphicsCardDto graphicsCard)
        {
            buffer.AppendLine($"{graphicsCard.Id},\"{graphicsCard.FullName},\"{graphicsCard.StockQuantity},\"{graphicsCard.IsSupportRtx}\"");
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<GraphicsCardDto>)
            {
                foreach (var graphicsCard in (IEnumerable<GraphicsCardDto>)context.Object)
                {
                    FormatCsv(buffer, graphicsCard);
                }
            }
            else
            {
                FormatCsv(buffer, (GraphicsCardDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }
    }
}
