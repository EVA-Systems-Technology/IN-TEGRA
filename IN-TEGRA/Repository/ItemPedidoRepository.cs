using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;

namespace IN_TEGRA.Repository
{
    public class ItemPedidoRepository : IItemPedidoRepository
    {

        private readonly string _conexaoMySQL;
        public ItemPedidoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void CadastrarItemPedido(int IdPedido, ItemPedido item)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO tbItemPedido (IdPedido, IdProd, QtdItemPedido, PrecoItemPedido) VALUES (@IdPedido, @IdProd, @QtdItemPedido, @PrecoItemPedido);", conexao);

                cmd.Parameters.AddWithValue("@IdPedido", IdPedido);
                cmd.Parameters.AddWithValue("@IdProd", item.IdProduto);
                cmd.Parameters.AddWithValue("@QtdItemPedido", item.QtdItemPedido);
                cmd.Parameters.AddWithValue("@PrecoItemPedido", item.ValorItemPedido);

                cmd.ExecuteNonQuery();


            }
        }
    }
}
