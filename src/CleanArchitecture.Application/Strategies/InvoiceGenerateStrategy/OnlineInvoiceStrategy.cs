using CleanArchitecture.Application.DTOs.OrderDto;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Companion;
using System.Globalization;

namespace CleanArchitecture.Application.Strategies.InvoiceGenerateStrategy
{
  public class OnlineInvoiceStrategy : IInvoiceGenerateStrategy
  {
    private IUnitOfWork _unitOfWork;

    public async Task<byte[]> GenerateAsync(Order order, IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;

      var cosmeticsDict = new Dictionary<Guid, Cosmetic>();
      var cosmeticPriceDict = new Dictionary<Guid, decimal>();
      var coupon = await _unitOfWork.Coupons.GetByIdAsync(order.CouponId);

      foreach (var item in order.OrderItems)
      {
        // Assume GetByIdAsync returns a Cosmetic instance
        var cosmetic = await _unitOfWork.Cosmetics.GetByIdAsync(item.CosmeticId);
        cosmeticsDict[item.CosmeticId] = cosmetic;

        var cosmeticPrice = await _unitOfWork.Cosmetics.GetCosmeticOriginalPrice(cosmeticsDict[item.CosmeticId]);
        cosmeticPriceDict[item.CosmeticId] = cosmeticPrice;
      }

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
            
            if (coupon is not null)
              column.Item().Text($"Coupon: {coupon.Name}");

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
                header.Cell().Element(CellStyle).Text("Cosmetic");
                header.Cell().Element(CellStyle).Text("Quantity");
                header.Cell().Element(CellStyle).Text("Unit Price");
                header.Cell().Element(CellStyle).Text("Total");
              });

              // Data Rows
              foreach (var item in order.OrderItems)
              {
                var cosmetic = cosmeticsDict[item.CosmeticId];
                // Assuming Cosmetic has Price and Name properties.
                decimal unitPrice = cosmeticPriceDict[item.CosmeticId];
                decimal lineTotal = unitPrice * item.Quantity;

                table.Cell().Element(CellStyle).Text(cosmetic.Name);
                table.Cell().Element(CellStyle).Text(item.Quantity.ToString());
                table.Cell().Element(CellStyle).Text($"{unitPrice:0,0}VND", TextStyle.Default.FontSize(10));
                table.Cell().Element(CellStyle).Text($"{lineTotal:0,0}VND", TextStyle.Default.FontSize(10));
              }
            });

            // Totals Section
            column.Item().Text(" ");
            column.Item().Text($"SubTotal: {order.SubTotal:0,0}VND", TextStyle.Default.SemiBold());
            column.Item().Text($"Total Price: {order.TotalPrice:0,0}VND", TextStyle.Default.SemiBold());
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