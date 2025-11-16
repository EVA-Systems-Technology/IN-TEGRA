using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using MySql.Data.MySqlClient;

namespace IN_TEGRA.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _conexaoMySQL;

        public PedidoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public int CadastrarPedido(Pedido pedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO tbPedido (IdCliente, FretePedido, ValorPedido, DataHoraPedido, ConfirmacaoPedido) VALUES (@IdCliente, @FretePedido, @ValorPedido, NOW(), true);   SELECT LAST_INSERT_ID();", conexao); // pega o ultimo id registrado para retornar

                cmd.Parameters.AddWithValue("@IdCliente", pedido.IdCliente);
                cmd.Parameters.AddWithValue("@FretePedido", pedido.FretePedido);
                cmd.Parameters.AddWithValue("@ValorPedido", pedido.ValorPedido);

                return Convert.ToInt32(cmd.ExecuteScalar()); // retorna o id do pedido cadastrado com sucesso
            }
        }

        public List<ItemPedido> ObterItensPedido(int IdPedido)
        {
            var pedidos = new List<ItemPedido>();

            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT item.IdItemPedido, item.IdProd, item.Quantidade, item.PrecoItemPedido, produto.NomeProd FROM tbitemPedido item JOIN tbProduto produto ON produto.IdProd = item.idProd WHERE item.IdPedido = @IdPedido", conexao);
                cmd.Parameters.AddWithValue("@IdPedido", IdPedido);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (dr.Read()) 
                {
                    var item = new ItemPedido()
                    {
                        IdItemPedido = Convert.ToInt32(dr["IdItemPedido"]),
                        IdPedido = IdPedido,
                        IdProduto = Convert.ToInt32(dr["IdProd"]),
                        QuantidadeItemPedido = Convert.ToInt32(dr["Quantidade"]),
                        ValorItemPedido= Convert.ToDouble(dr["PrecoItemPedido"])

                    };
                    pedidos.Add(item);

                }
                return pedidos;

            }
        }

        public Pedido ObterPedidoPorId(int IdPedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbPedido WHERE IdPedido = @IdPedido", conexao);
                cmd.Parameters.AddWithValue("@IdPedido", IdPedido);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                Pedido pedido = new Pedido();

                while (dr.Read())
                {
                    pedido.IdPedido = Convert.ToInt32(dr["IdPedido"]);
                    pedido.IdCliente = Convert.ToInt32(dr["IdCliente"]);
                    pedido.FretePedido = Convert.ToDouble(dr["FretePedido"]);
                    pedido.ValorPedido = Convert.ToDouble(dr["ValorPedido"]);
                    pedido.DataHoraPedido = Convert.ToDateTime(dr["DataHoraPedido"]);
                    pedido.ConfirmacaoPedido = Convert.ToBoolean(dr["ConfirmacaoPedido"]);

                }
                if (pedido != null) 
                {
                    pedido.PedidoItens = ObterItensPedido(pedido.IdPedido);
                }
                return pedido;
            }
        }

        public List<Pedido> ObterPedidosCliente(int IdCliente)
        {
            var pedidos = new List<Pedido>();

            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbPedido WHERE IdCliente = @IdCliente;", conexao);

                cmd.Parameters.AddWithValue("@IdCliente", IdCliente);


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (dr.Read()) 
                {                    
                    var pedido = new Pedido()
                    {
                        IdPedido = Convert.ToInt32(dr["IdPedido"]),
                        IdCliente = Convert.ToInt32(dr["IdCliente"]),
                        FretePedido = Convert.ToDouble(dr["FretePedido"]),
                        ValorPedido = Convert.ToDouble(dr["ValorPedido"]),
                        DataHoraPedido = Convert.ToDateTime(dr["DataHoraPedido"]),
                        ConfirmacaoPedido = Convert.ToBoolean(dr["ConfirmacaoPedido"])
                    };

                    pedido.PedidoItens = ObterItensPedido(pedido.IdPedido);
                    pedidos.Add(pedido);
                }
                return pedidos;

            }
        }
    }
}
