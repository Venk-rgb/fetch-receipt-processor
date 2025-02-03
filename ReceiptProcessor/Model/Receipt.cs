namespace ReceiptProcessor.Model
{
    public class Receipt
    {
        public string Retailer { get; set; }
        public DateTime PurchaseDate { get; set; }
        
        public string PurchaseTime { get; set; } 

        public double Total { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string ShortDescription { get; set; }
        public double Price { get; set; }
    }
}