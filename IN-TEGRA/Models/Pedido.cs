namespace IN_TEGRA.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public double ValorPedido { get; set; }
        public double FretePedido { get; set; }
        public DateTime DataHoraPedido { get; set; }
        public bool ConfirmacaoPedido { get; set; }

        // Array com os produtos dentro do pedido
        public List<ItemPedido> PedidoItens { get; set; }
    }
}
