using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IClienteRepository
    {
        //login cliente
        Cliente Login(string Email, string Senha);

        IEnumerable<Cliente> ObterTodosClientes();

        void CadastrarCliente(Cliente cliente);

        void AtualizarCliente(Cliente cliente);

        Cliente ObterClientePorId(int IdCliente);

        void ExcluirCliente(int IdCliente);
    }
}
