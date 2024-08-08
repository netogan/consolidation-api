using Consolidation.Api.Domain.Services.Interfaces;
using Consolidation.Api.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Consolidation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolidationController : Controller
    {
        private readonly IConsolidationService _consolidationService;
        private readonly PdfService _pdfService;

        public ConsolidationController(IConsolidationService consolidationService, PdfService pdfService)
        {
            _consolidationService = consolidationService;
            _pdfService = pdfService;
        }


        [HttpPost("processar")]
        public async Task<ActionResult> ProcessingConsolidation(DateOnly startDate, DateOnly finalDate)
        {
            for(var day = startDate; day <= finalDate; day = day.AddDays(1))
            {
                await _consolidationService.ProcessConsolidation(day);
            }
            return Ok();
        }

        [HttpGet("report")]
        public async Task<ActionResult> GetReport(DateOnly startDate, DateOnly finalDate)
        {
            var consolidations = await _consolidationService.GetConsolidationByRange(startDate, finalDate);

            if(consolidations == null)
            {
                return NotFound();
            }

            var pdf = await _pdfService.GeneratePdf(startDate, finalDate, consolidations);

            return File(pdf, "application/pdf", $"consolidation_report_{startDate:ddMMyyyy}_{finalDate:ddMMyyyy}.pdf");
        }
    }
}
