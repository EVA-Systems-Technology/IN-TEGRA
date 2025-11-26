using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> ObterTodosClientes();

        void CadastrarCliente(Cliente cliente);

        void AtualizarCliente(Cliente cliente);

        Cliente ObterClientePorId(int IdCli);

        void ExcluirCliente(int IdCli);
    }
}
