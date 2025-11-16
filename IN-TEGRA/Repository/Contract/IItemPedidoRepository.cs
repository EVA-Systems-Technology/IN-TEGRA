using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IItemPedidoRepository
    {
        void CadastrarItemPedido(int IdPedido, ItemPedido item);
    }
}
