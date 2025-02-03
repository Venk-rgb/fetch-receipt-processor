using Microsoft.AspNetCore.Mvc;
using ReceiptProcessor.Controllers;
using ReceiptProcessor.Model;
using Xunit;

namespace ReceiptProcessor.Tests
{
    public class ReceiptsControllerTests
    {
        private readonly ReceiptsController _controller = new();

        [Fact]
        public void ProcessReceipt_ValidReceipt_ReturnsId()
        {
            var receipt = new Receipt
            {
                Retailer = "Walmart",
                Total = 100.00,
                Items =
                [
                    new() { ShortDescription = "Item A", Price = 20.00 },
                    new() { ShortDescription = "Item B", Price = 30.00 }
                ],
                PurchaseDate = DateTime.Parse("2024-02-01"),
                PurchaseTime = "14:30"
            };

            var result = _controller.ProcessReceipt(receipt) as OkObjectResult;
            
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.Contains("id", result.Value?.ToString());
        }

    }   
}