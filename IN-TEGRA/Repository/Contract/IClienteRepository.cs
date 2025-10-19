using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> ObterTodosClientes();

        void CadastrarCliente(Cliente cliente);

        void AtualizarCliente(Cliente cliente);

        void ObterClientePorId(int IdCliente);

        void ExcluirCliente(int IdCliente);
    }
}
