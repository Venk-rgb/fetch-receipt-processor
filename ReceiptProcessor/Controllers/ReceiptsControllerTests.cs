using Microsoft.AspNetCore.Mvc;
using ReceiptProcessor.Model;
using Xunit;

namespace ReceiptProcessor.Controllers
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
        
        [Fact]
        public void ProcessReceipt_NullReceipt_ReturnsBadRequest()
        {
            var result = _controller.ProcessReceipt(null) as BadRequestObjectResult;
            
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("The receipt is invalid.", result.Value);
        }

        [Fact]
        public void GetPoints_InvalidReceiptId_ReturnsNotFound()
        {
            var result = _controller.GetPoints("invalid-id") as NotFoundObjectResult;
            
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("No receipt found for that ID.", result.Value);
        }

        [Fact]
        public void CalculatePoints_ValidReceipt_CalculatesCorrectly()
        {
            var receipt = new Receipt
            {
                Retailer = "Target",
                Total = 35.35,
                Items =
                [
                    new Item { ShortDescription = "Mountain Dew 12PK", Price = 6.49 },
                    new Item { ShortDescription = "Emils Cheese Pizza", Price = 12.25 },
                    new Item { ShortDescription = "Knorr Creamy Chicken", Price = 1.26 },
                    new Item { ShortDescription = "Doritos Nacho Cheese", Price = 3.35 },
                    new Item { ShortDescription = "   Klarbrunn 12-PK 12 FL OZ  ", Price = 12.00 }
                ],
                PurchaseDate = DateTime.Parse("2022-01-01"),
                PurchaseTime = "13:01"
            };

            var points = _controller.GetType()
                .GetMethod("CalculatePoints", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.Invoke(_controller, [receipt]);
            
            Assert.IsType<int>(points);
            Assert.True((int)points > 0);
            Assert.Equal(28, points);
        }
    }   
}