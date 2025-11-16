using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using MySql.Data.MySqlClient;

namespace IN_TEGRA.Repository
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly string _conexaoMySQL;

        public PagamentoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public int RegistrarPagamento(Pagamento pagamento)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO tbPagamento (IdPedido, IdCliente, ValorPagamento, TipoPagamento, DataHoraPagamento) VALUES (@IdPedido, @IdCliente, @ValorPagamento, @TipoPagamento, NOW()); SELECT LAST_INSERT_ID();", conexao);

                cmd.Parameters.AddWithValue("@IdPedido", pagamento.IdPedido);
                cmd.Parameters.AddWithValue("@IdCliente", pagamento.IdCliente);
                cmd.Parameters.AddWithValue("@ValorPagamento", pagamento.ValorPagamento);
                cmd.Parameters.AddWithValue("@TipoPagamento", pagamento.TipoPagamento);

                var IdpagamentoGerado = Convert.ToInt32(cmd.ExecuteScalar());
                pagamento.IdPagamento = IdpagamentoGerado;

                MySqlCommand cmdConfirmacao = new MySqlCommand("UPDATE tbPedido SET ConfirmacaoPedido = @Confirmacao WHERE IdPedido = @IdPedido;", conexao);

                cmdConfirmacao.Parameters.AddWithValue("@Confirmacao", true);
                cmdConfirmacao.Parameters.AddWithValue("@IdPedido", pagamento.IdPedido);
                cmdConfirmacao.ExecuteNonQuery();



            }
            return pagamento.IdPagamento;
        }
    }
}
