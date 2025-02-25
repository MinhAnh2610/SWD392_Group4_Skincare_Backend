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
        async (IReportService service, [FromQuery] string? format, [FromQuery] string? type,
          [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate) =>
        {
          GenerateReportRequest request = new(format, type, fromDate, toDate);
          var result = await service.GenerateReport(request);
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