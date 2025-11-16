using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IPedidoRepository
    {
        int CadastrarPedido(Models.Pedido pedido);
        Pedido ObterPedidoPorId(int IdPedido);
        List<Pedido> ObterPedidosCliente(int IdCliente);
        public List<ItemPedido> ObterItensPedido(int IdPedido);
    }
}
