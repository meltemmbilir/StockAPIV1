namespace StockAPI.Model
{
    public class Variant : Base
    {
        public int ProductId { get; set; }
        public string? VariantCode { get; set; }  //model , size , color
        public int Quantity { get; set; } // stocktaki miktarı 
    }
}
