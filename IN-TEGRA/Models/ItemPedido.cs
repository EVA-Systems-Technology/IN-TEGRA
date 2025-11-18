namespace IN_TEGRA.Models
{
    public class ItemPedido
    {
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public int QtdItemPedido { get; set; }
        public double ValorItemPedido { get; set; }
        public double ValorTotalPedido => QtdItemPedido * ValorItemPedido;
    }
}
