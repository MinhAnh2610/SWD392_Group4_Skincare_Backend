using CleanArchitecture.Application.Constants;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using CleanArchitecture.Application.DTOs.ReportDto;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Companion;
using System.Globalization;

namespace CleanArchitecture.Application.Strategies
{
  public class PdfReportGenerateStrategy : IReportGenerateStrategy
  {
    public byte[] Generate(GenerateReportRequest request, ReportContent reportContent)
    {
      var document = Document.Create(container =>
      {
        container.Page(page =>
        {
          page.Size(PageSizes.A4);
          page.Margin(2, Unit.Centimetre);
          page.PageColor(Colors.White);
          page.DefaultTextStyle(x => x.FontSize(12));

          // Header
          page.Header()
            .Text($"{GetHeader(request.Type)}")
            .SemiBold().FontSize(20).FontColor(Colors.Black)
            .AlignCenter();

          // Content
          page.Content().PaddingVertical(1, Unit.Centimetre).Column(column =>
          {
            column.Spacing(20);

            // Date Range and Total Revenue
            column.Item()
              .Text($"Report Period: {reportContent.StartDate:MM/dd/yyyy} - {reportContent.ToDate:MM/dd/yyyy}");
            column.Item().Text($"Total Revenue: {reportContent.TotalRevenue:C}", TextStyle.Default.SemiBold());

            // Table Header
            column.Item().Text("Cosmetics Sold Details").SemiBold().FontSize(16);

            // Table
            column.Item().Table(table =>
            {
              table.ColumnsDefinition(columns =>
              {
                columns.RelativeColumn(2); // Cosmetic Id
                columns.RelativeColumn(3); // Cosmetic Name
                columns.RelativeColumn(2); // Number Of Sales
                columns.RelativeColumn(2); // Revenue
              });

              // Header row
              table.Header(header =>
              {
                header.Cell().Element(CellStyle).Text("Cosmetic Id");
                header.Cell().Element(CellStyle).Text("Cosmetic Name");
                header.Cell().Element(CellStyle).Text("Number Of Sales");
                header.Cell().Element(CellStyle).Text("Revenue");
              });

              // Data rows
              foreach (var cosmetic in reportContent.CosmeticsSold)
              {
                table.Cell().Element(CellStyle).Text(cosmetic.CosmeticId.ToString());
                table.Cell().Element(CellStyle).Text(cosmetic.CosmeticName);
                table.Cell().Element(CellStyle).Text(cosmetic.NumberOfSales.ToString());
                table.Cell().Element(CellStyle).Text(cosmetic.Revenue.ToString("C", CultureInfo.CurrentCulture));
              }
            });
          });

          // Footer
          page.Footer()
            .AlignCenter()
            .Text(x =>
            {
              x.Span("Page ");
              x.CurrentPageNumber();
            });
        });
      });

      // Optionally, uncomment the next line to preview in QuestPDF Companion.
      document.ShowInCompanion(12500);

      byte[] fileBytes = document.GeneratePdf();
      return fileBytes;
    }

    private static string GetHeader(string reportType)
    {
      return reportType switch
      {
        "revenue" => "Revenue Report",
        "product_performance" => "Product Performance Report",
        _ => "Report"
      };
    }

    // Helper method for styling table cells.
    static IContainer CellStyle(IContainer container)
    {
      return container.Padding(5)
        .Border(1)
        .BorderColor(Colors.Grey.Lighten2);
    }
  }
}