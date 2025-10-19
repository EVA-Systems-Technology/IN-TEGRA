namespace IN_TEGRA.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int CpfCliente { get; set; }
        public bool PedidoConfirmado { get; set; }
        public double ValorTotal { get; set; }
        public double Frete { get; set; }
    }
}
