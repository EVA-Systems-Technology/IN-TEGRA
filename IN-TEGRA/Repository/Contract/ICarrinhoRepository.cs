using IN_TEGRA.Models;
using Org.BouncyCastle.Asn1.Cmp;

namespace IN_TEGRA.Repository.Contract
{
    public interface ICarrinhoRepository
    {
        List<ItemCarrinho> ItensCarrinho(ISession session);
        void AddCarrinho(ISession session, Produto produto, int Quantidade);
        void AlterarQuantidadeItem(ISession session, int ProdutoId, int NovaQuantidade);
        void RemoverItem(ISession session, int ProdutoId);
        void LimparCarrinho(ISession session);
        decimal TotalCarrinho(ISession session);

    }
}
