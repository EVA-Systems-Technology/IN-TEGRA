using Newtonsoft.Json;
using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using System.Runtime.InteropServices;

namespace IN_TEGRA.Repository
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private const string CartSessionKey = "CartItems";

        public void AddCarrinho(ISession session, Produto produto, int Quantidade)
        {
            var cart = ItensCarrinho(session);
            var existingItem = cart.FirstOrDefault(item => item.ProdId == produto.IdProd);

            if (existingItem != null)
            {
                existingItem.QuantidadeProd += Quantidade;
            }
            else
            {
                cart.Add(new ItemCarrinho
                {
                    ProdId = produto.IdProd,
                    //Produto = produto,
                    QuantidadeProd = Quantidade,
                    PrecoProd = (decimal)produto.PrecoProduto
                });
            }
            SalvarCarrinho(session, cart);

        }

        public void AlterarQuantidadeItem(ISession session, int ProdutoId, int NovaQuantidade)
        {
            var cart = ItensCarrinho(session);
            var itemAlterar = cart.FirstOrDefault(item => item.ProdId == ProdutoId);

            if (itemAlterar != null)
            {
                if (NovaQuantidade <= 0)
                {
                    cart.Remove(itemAlterar);
                }
                else
                {
                    itemAlterar.QuantidadeProd = NovaQuantidade;
                }
                SalvarCarrinho(session, cart);
            }
        }

        public List<ItemCarrinho> ItensCarrinho(ISession session)
        {
           var CartJson = session.GetString(CartSessionKey);
           return CartJson == null ? new List<ItemCarrinho>() : JsonConvert.DeserializeObject<List<ItemCarrinho>>(CartJson);
        }

        public void LimparCarrinho(ISession session)
        {
            session.Remove(CartSessionKey);
        }

        public void RemoverItem(ISession session, int ProdutoId)
        {
            var cart = ItensCarrinho(session);
            var itemRemover = cart.FirstOrDefault(item => item.ProdId == ProdutoId);
            if (itemRemover != null)
            {
                cart.Remove(itemRemover);
                SalvarCarrinho(session, cart);
            }
        }

        public decimal TotalCarrinho(ISession session)
        {
            return ItensCarrinho(session).Sum(item => item.Total);  
        }

        private void SalvarCarrinho(ISession session, List<ItemCarrinho> cart)
        {
            session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
        }
    }
}
