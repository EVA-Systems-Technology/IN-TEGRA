using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IEnderecoRepository
    {
        void CadastrarEndereco(Endereco endereco);
        void AtualizarEndereco(Endereco endereco);
        void ExcluirEndereco(int Id);
        Endereco ObterEnderecoPorId(int IdEndereco);
    }
}
