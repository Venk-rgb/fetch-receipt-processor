using Microsoft.AspNetCore.Mvc;
using ReceiptProcessor.Model;
using System;
using System.Collections.Generic;

namespace ReceiptProcessor.Controllers
{
    [ApiController]
    [Route("receipts")]
    public class ReceiptsController : ControllerBase
    {
        private static readonly Dictionary<string, Receipt> receipts = new();

        [HttpPost("process")]
        public IActionResult ProcessReceipt([FromBody] Receipt receipt)
        {
            if (receipt == null)
            {
                return BadRequest("The receipt is invalid.");
            }

            var receiptId = Guid.NewGuid().ToString();
            receipts[receiptId] = receipt;

            return Ok(new { id = receiptId });
        }
        
        [HttpGet("{id}/points")]
        public IActionResult GetPoints(string id)
        {
            if (!receipts.TryGetValue(id, out var receipt))
            {
                return NotFound("No receipt found for that ID.");
            }

            int points = CalculatePoints(receipt);
            return Ok(new { points });
        }

        private int CalculatePoints(Receipt receipt)
        {
            int points = 0;
            
            points += receipt.Retailer.Count(char.IsLetterOrDigit);
            
            if (receipt.Total % 1 == 0)
                points += 50;
            
            if (receipt.Total % 0.25 == 0)
                points += 25;
            
            points += (receipt.Items.Count / 2) * 5;
            
            foreach (var item in receipt.Items)
            {
                int descriptionLength = item.ShortDescription.Trim().Length;
                if (descriptionLength % 3 == 0)
                {
                    points += (int)Math.Ceiling(item.Price * 0.2);
                }
            }

            if (receipt.PurchaseDate.Day % 2 != 0)
                points += 6;
            
            if (TimeSpan.TryParseExact(receipt.PurchaseTime, @"hh\:mm", null, out TimeSpan purchaseTime))
            {
                if (purchaseTime >= TimeSpan.FromHours(14) && purchaseTime < TimeSpan.FromHours(16))
                {
                    points += 10;
                }
            }

            return points;
        }
    }
}