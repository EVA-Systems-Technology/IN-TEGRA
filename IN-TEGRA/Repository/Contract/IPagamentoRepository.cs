using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IPagamentoRepository
    {
        int RegistrarPagamento(Pagamento pagamento);
    }
}
