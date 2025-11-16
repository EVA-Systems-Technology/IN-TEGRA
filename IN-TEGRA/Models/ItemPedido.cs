namespace IN_TEGRA.Models
{
    public class ItemPedido
    {
        public int IdItemPedido { get; set; }
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public int QuantidadeItemPedido { get; set; }
        public double ValorItemPedido { get; set; }
        public double ValorTotalPedido => QuantidadeItemPedido * ValorItemPedido;
    }
}
