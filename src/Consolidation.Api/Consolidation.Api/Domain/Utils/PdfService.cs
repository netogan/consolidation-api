using iText.Html2pdf;
using Stubble.Core.Builders;

namespace Consolidation.Api.Domain.Utils
{
    public class PdfService
    {
        public async Task<byte[]> GeneratePdf(DateOnly startDate, DateOnly finalDate, IEnumerable<Models.Consolidation> consolidations)
        {
            var stubble = new StubbleBuilder().Build();

            var template = File.ReadAllText(".\\Domain\\Utils\\Resources\\template.html");

            consolidations = consolidations.OrderBy(x => x.DateCreateAt);

            var data = new
            {
                StartDate = startDate.ToString("dd-MM-yyyy"),
                EndDate = finalDate.ToString("dd-MM-yyyy"),
                Consolidations = consolidations.Select(c => new
                {
                    Date = c.DateCreateAt.ToString("dd-MM-yyyy"),
                    InitialBalance = c.OpeningBalance.ToString("C"),
                    FinalBalance = c.FinalBalance.ToString("C")
                }).ToList()
            };

            var html = stubble.Render(template, data);

            using (var stream = new MemoryStream())
            {
                var converterProperties = new ConverterProperties();
                HtmlConverter.ConvertToPdf(html, stream, converterProperties);
                return stream.ToArray();
            }
        }
    }
}
