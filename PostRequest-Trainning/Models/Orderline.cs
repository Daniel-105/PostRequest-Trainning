namespace PostRequest_Trainning.Models
{
    public class Orderline
    {
        public string Type { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int TaxRate { get; set; }
        public int TotalAmount { get; set; }
        public int TotalDiscountAmount { get; set; }
        public int TotalTaxAmount { get; set; }
    }
}
