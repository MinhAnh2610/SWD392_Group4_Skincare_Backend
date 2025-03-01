using CleanArchitecture.Application.DTOs.OrderDto;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Companion;
using System.Globalization;

namespace CleanArchitecture.Application.Strategies.InvoiceGenerateStrategy
{
  public class OnlineInvoiceStrategy: IInvoiceGenerateStrategy
  {
    public byte[] Generate(Order order)
    {
      var document = Document.Create(container =>
      {
        container.Page(page =>
        {
          page.Size(PageSizes.A4);
          page.Margin(2, Unit.Centimetre);
          page.PageColor(Colors.White);
          page.DefaultTextStyle(x => x.FontSize(12));

          // Header: Invoice Title
          page.Header()
            .Text("Invoice")
            .SemiBold()
            .FontSize(20)
            .FontColor(Colors.Black)
            .AlignCenter();

          // Content: Order details and items table
          page.Content().Column(column =>
          {
            column.Spacing(10);

            // Order details section
            column.Item().Text($"Invoice Date: {order.OrderDate:MM/dd/yyyy}");
            column.Item().Text($"Order ID: {order.Id}");
            column.Item().Text($"Customer ID: {order.CustomerId}");
            column.Item().Text($"Tracking Number: {order.TrackingNumber}");
            column.Item().Text($"Delivery Date: {order.DeliveryDate:MM/dd/yyyy}");
            column.Item().Text($"Shipping Address: {order.ShippingAddress}");
            column.Item().Text($"Billing Address: {order.BillingAddress}");
            column.Item().Text($"Status: {order.Status}");

            // Spacer
            column.Item().Text(" ");

            // Order Items Table Title
            column.Item().Text("Order Items").SemiBold().FontSize(16);

            // Order Items Table
            column.Item().Table(table =>
            {
              table.ColumnsDefinition(columns =>
              {
                columns.RelativeColumn(3); // Cosmetic Name
                columns.RelativeColumn(1); // Quantity
                columns.RelativeColumn(2); // Unit Price
                columns.RelativeColumn(2); // Line Total
              });

              // Header Row
              table.Header(header =>
              {
                header.Cell().Element(CellStyle).Text("Cosmetic Name");
                header.Cell().Element(CellStyle).Text("Quantity");
                header.Cell().Element(CellStyle).Text("Unit Price");
                header.Cell().Element(CellStyle).Text("Line Total");
              });

              // Data Rows
              foreach (var item in order.OrderItems)
              {
                // Assuming Cosmetic has Price and Name properties.
                decimal unitPrice = item.Cosmetic.Price;
                decimal lineTotal = unitPrice * item.Quantity;

                table.Cell().Element(CellStyle).Text(item.Cosmetic.Name);
                table.Cell().Element(CellStyle).Text(item.Quantity.ToString());
                table.Cell().Element(CellStyle).Text($"{unitPrice:C}", TextStyle.Default.FontSize(10));
                table.Cell().Element(CellStyle).Text($"{lineTotal:C}", TextStyle.Default.FontSize(10));
              }
            });

            // Totals Section
            column.Item().Text(" ");
            column.Item().Text($"SubTotal: {order.SubTotal:C}", TextStyle.Default.SemiBold());
            column.Item().Text($"Total Price: {order.TotalPrice:C}", TextStyle.Default.SemiBold());
          });

          // Footer: Page number
          page.Footer()
            .AlignCenter()
            .Text(x =>
            {
              x.Span("Page ");
              x.CurrentPageNumber();
            });
        });
      });

      // Uncomment the following line to preview the document during development:
      // document.ShowInCompanion();

      return document.GeneratePdf();
    }

    // Helper method for consistent table cell styling.
    static IContainer CellStyle(IContainer container)
    {
      return container.Padding(5)
        .Border(1)
        .BorderColor(Colors.Grey.Lighten2);
    }
  }
}