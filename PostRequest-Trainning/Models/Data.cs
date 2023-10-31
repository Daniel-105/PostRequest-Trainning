namespace PostRequest_Trainning.Models
{
    public class Data
    {
        public string purchase_country { get; set; }
        public string purchase_currency { get; set; }
        public string locale { get; set; }
        public int order_amount { get; set; }
        public int order_tax_amount { get; set; }
        public List<Orderline> order_lines { get; set; }
    }
}
