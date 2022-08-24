namespace StockAPI.Model
{
    public class Product : Base
    {
        public string? ProductCode { get; set; }
        public List<Variant> Variants { get; set; }
    }
}
