using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IEnderecoRepository
    {
        void CadastrarEndereco(Endereco endereco);
        Endereco ObterEnderecoPorId(int IdEndereco);
    }
}
