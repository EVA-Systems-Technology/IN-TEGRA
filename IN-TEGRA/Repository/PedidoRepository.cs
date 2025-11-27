using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

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

                MySqlCommand cmd = new MySqlCommand("INSERT INTO tbPedido (IdCli, FretePedido, ValorPedido, DataHoraPedido, ConfirmacaoPedido, IdEndereco) VALUES (@IdCli, @FretePedido, @ValorPedido, NOW(), true, @IdEndereco);   SELECT LAST_INSERT_ID();", conexao); // pega o ultimo id registrado para retornar

                cmd.Parameters.AddWithValue("@IdCli", pedido.IdCli);
                cmd.Parameters.AddWithValue("@FretePedido", pedido.FretePedido);
                cmd.Parameters.AddWithValue("@ValorPedido", pedido.ValorPedido);
                cmd.Parameters.AddWithValue("@IdEndereco", pedido.IdEndereco);


                return Convert.ToInt32(cmd.ExecuteScalar()); // retorna o id do pedido cadastrado com sucesso
            }
        }

        public void ExcluirPedido(int IdPedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmdPagamento = new MySqlCommand("delete from tbpagamento where IdPedido=@IdPedido", conexao);
                cmdPagamento.Parameters.AddWithValue("@IdPedido", IdPedido);
                cmdPagamento.ExecuteNonQuery();

                MySqlCommand cmdItem = new MySqlCommand("delete from tbitempedido where IdPedido=@IdPedido", conexao);
                cmdItem.Parameters.AddWithValue("@IdPedido", IdPedido);
                cmdItem.ExecuteNonQuery();

                MySqlCommand cmd = new MySqlCommand("Delete from tbPedido where IdPedido=@IdPedido", conexao);
                cmd.Parameters.AddWithValue("@IdPedido", IdPedido);
                cmd.ExecuteNonQuery();
            }
        }

        public List<ItemPedido> ObterItensPedido(int IdPedido)
        {
            var pedidos = new List<ItemPedido>();

            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT item.IdProd, item.QtdItemPedido, item.PrecoItemPedido, produto.NomeProd FROM tbitemPedido item JOIN tbProduto produto ON produto.IdProd = item.idProd WHERE item.IdPedido = @IdPedido", conexao);
                cmd.Parameters.AddWithValue("@IdPedido", IdPedido);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (dr.Read()) 
                {
                    var item = new ItemPedido()
                    {
                        IdPedido = IdPedido,
                        NomeProduto = dr["NomeProd"].ToString(),
                        IdProduto = Convert.ToInt32(dr["IdProd"]),
                        QtdItemPedido = Convert.ToInt32(dr["QtdItemPedido"]),
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
                    pedido.IdCli = Convert.ToInt32(dr["IdCli"]);
                    pedido.FretePedido = Convert.ToDouble(dr["FretePedido"]);
                    pedido.ValorPedido = Convert.ToDouble(dr["ValorPedido"]);
                    pedido.DataHoraPedido = Convert.ToDateTime(dr["DataHoraPedido"]);
                    pedido.ConfirmacaoPedido = Convert.ToBoolean(dr["ConfirmacaoPedido"]);
                    pedido.IdEndereco = Convert.ToInt32(dr["IdEndereco"]);

                }
                if (pedido != null) 
                {
                    pedido.PedidoItens = ObterItensPedido(pedido.IdPedido);
                }
                return pedido;
            }
        }

        public List<Pedido> ObterPedidosCliente(int IdCli)
        {
            var pedidos = new List<Pedido>();

            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbPedido WHERE IdCli = @IdCli;", conexao);

                cmd.Parameters.AddWithValue("@IdCli", IdCli);

                MySqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (dr.Read()) 
                {                    
                    var pedido = new Pedido()
                    {
                        IdPedido = Convert.ToInt32(dr["IdPedido"]),
                        IdCli = Convert.ToInt32(dr["IdCli"]),
                        FretePedido = Convert.ToDouble(dr["FretePedido"]),
                        ValorPedido = Convert.ToDouble(dr["ValorPedido"]),
                        DataHoraPedido = Convert.ToDateTime(dr["DataHoraPedido"]),
                        ConfirmacaoPedido = Convert.ToBoolean(dr["ConfirmacaoPedido"]),
                        IdEndereco = Convert.ToInt32(dr["IdEndereco"])
                    };

                    pedido.PedidoItens = ObterItensPedido(pedido.IdPedido);
                    pedidos.Add(pedido);
                }
                return pedidos;

            }
        }

        public IEnumerable<Pedido> ObterTodosPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM TBPEDIDO",conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    pedidos.Add(new Pedido
                    {
                        IdPedido = Convert.ToInt32(dr["IdPedido"]),
                        IdCli = Convert.ToInt32(dr["IdCli"]),
                        FretePedido = Convert.ToDouble(dr["FretePedido"]),
                        ValorPedido = Convert.ToDouble(dr["ValorPedido"]),
                        DataHoraPedido = Convert.ToDateTime(dr["DataHoraPedido"]),
                        ConfirmacaoPedido = Convert.ToBoolean(dr["ConfirmacaoPedido"]),
                        IdEndereco = Convert.ToInt32(dr["IdEndereco"])
                    });

                }
                return pedidos;
            }
        }
    }
}

           