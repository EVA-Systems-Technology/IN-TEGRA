using IN_TEGRA.Models;

namespace IN_TEGRA.Repository.Contract
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> ObterTodosProdutos();

        void CadastrarProduto(Produto produto);

        void AtualizarProduto(Produto produto);

        Produto ObterProdutoPorId(int IdProduto);

        void ExcluirProduto(int IdProduto);
        
        IEnumerable<Produto> PesquisarProduto(string searchTerm);
    }
}
