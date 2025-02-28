using CleanArchitecture.Application.DTOs.ReportDto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.Presentation.Endpoints
{
  public class ReportController : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      var group = app.MapGroup("api/reports").WithTags("Reports Management");

      #region Generate Report

      group.MapGet("/",
          async (IReportService service, [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate, [FromQuery] string? format = "pdf",
            [FromQuery] string? type = "revenue") =>
          {
            // Assign default values if not provided - for development
            var startDate = fromDate ?? new DateTime(2025, 1, 14);
            var endDate = toDate ?? new DateTime(2025, 1, 18);

            GenerateReportRequest request = new(format, type, startDate, endDate);
            var result = await service.GenerateReportAsync(request);
            if (result.IsSuccess)
              return Results.File(result.Data, "application/pdf", "report.pdf");

            return result.Match("Test");
          })
        .WithName("CreateReport")
        .Produces<ApiResponse<byte[]>>()
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("CreateReports")
        .WithDescription("Create a Report");

      #endregion
    }
  }
}