using BarberBoss.Application.UseCases.Reports;
using BarberBoss.Domain.Reports;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BarberBoss.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {




        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExcel([FromServices ]IGenerateBillReportUseCase useCase, [FromHeader] DateOnly monh)
        {
            byte[] file = await useCase.Execute(monh);

            if (file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Octet, "report.xlsl");


            }

            return NoContent();
        }

        [HttpGet("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPdf([FromServices] IGenerateExpenseReportPdfUseCase useCase, [FromQuery] DateOnly monh)
        {
            byte[] file = await useCase.Execute(monh);

            if (file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
            }

            return NoContent();
        }
    }
}
