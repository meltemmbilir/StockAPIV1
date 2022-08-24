namespace StockAPI.Model
{
    public class Stock : Base
    {
        public string ProductCode { get; set; }
        public string VariantCode { get; set; }
        public int Quantity { get; set; }
    }
}
