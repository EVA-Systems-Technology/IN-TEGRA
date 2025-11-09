using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IFuncionarioRepository
    {
        Funcionario Login(string Email, string Senha);

        void cadastrarFuncionario(Funcionario funcionario);
        void atualizarFuncionario(Funcionario funcionario);
        void atualizarSenhaFuncionario(Funcionario funcionario);
        void excluirFuncionario(int IdFuncionario);

        Funcionario obterFuncionarioPorId(int IdFuncionario);
        List<Funcionario> obterFuncionarioPorEmail(string Email);
        IEnumerable<Funcionario> obterTodosFuncionarios();
    }
}
