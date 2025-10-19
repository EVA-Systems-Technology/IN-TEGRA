using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;

namespace IN_TEGRA.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _conexaoMySQL;

        public ClienteRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void AtualizarCliente(Models.Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public void CadastrarCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public void ExcluirCliente(int IdCliente)
        {
            throw new NotImplementedException();
        }

        public void ObterClientePorId(int IdCliente)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            throw new NotImplementedException();
        }
    }
}
