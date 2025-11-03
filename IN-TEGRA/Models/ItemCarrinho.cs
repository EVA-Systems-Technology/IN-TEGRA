namespace IN_TEGRA.Models
{
    public class ItemCarrinho
    {
        public int ProdId { get; set; }
        public Produto Produto { get; set; }
        public int QuantidadeProd { get; set; }
        public decimal PrecoProd { get; set; }
        public decimal Total => QuantidadeProd * PrecoProd;
    }
}
