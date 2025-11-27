using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IPedidoRepository
    {
        int CadastrarPedido(Models.Pedido pedido);
        Pedido ObterPedidoPorId(int IdPedido);
        List<Pedido> ObterPedidosCliente(int IdCli);
        IEnumerable<Pedido> ObterTodosPedidos();
        void ExcluirPedido(int IdPedido);
        public List<ItemPedido> ObterItensPedido(int IdPedido);
    }
}
