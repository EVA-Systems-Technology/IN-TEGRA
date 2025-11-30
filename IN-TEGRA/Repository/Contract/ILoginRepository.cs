using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface ILoginRepository
    {
        Cliente LoginComum(string Email, string Senha);
        Funcionario LoginFuncionario(string Email, string Senha);
        Login ObterUltimoLoginCliente(int IdCliente);
    }
}
