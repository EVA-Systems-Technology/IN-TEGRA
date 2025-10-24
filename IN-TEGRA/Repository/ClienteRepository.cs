using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Data;

namespace IN_TEGRA.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _conexaoMySQL;

        public ClienteRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void AtualizarCliente(Models.Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Update tbCliente set CpfCli=@CpfCli, NomeCli=@NomeCli, EmailCli=@EmailCli, TelefoneCli=@TelefoneCli, SenhaCli=@SenhaCli, NascCli=@NascCli where IdCliente=@IdCliente;", conexao);

                cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                cmd.Parameters.AddWithValue("@CpfCli", cliente.CpfCliente);
                cmd.Parameters.AddWithValue("@NomeCli", cliente.NomeCliente);
                cmd.Parameters.AddWithValue("@EmailCli", cliente.EmailCliente);
                cmd.Parameters.AddWithValue("@TelefoneCli", cliente.TelefoneCliente);
                cmd.Parameters.AddWithValue("@SenhaCli", cliente.SenhaCliente);
                cmd.Parameters.AddWithValue("@NascCli", cliente.DataNascimentoCliente.ToString("yyyy-MM-dd"));
                cmd.ExecuteNonQuery();
                conexao.Close();



            }
        }

        public void CadastrarCliente(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbCliente(CpfCli, NomeCli, EmailCli, TelefoneCli, SenhaCli, NascCli) values(@CpfCli, @NomeCli, @EmailCli, @TelefoneCli, @SenhaCli, @NascCli)", conexao);
            
                cmd.Parameters.AddWithValue("@CpfCli", cliente.CpfCliente);
                cmd.Parameters.AddWithValue("@NomeCli", cliente.NomeCliente);
                cmd.Parameters.AddWithValue("@EmailCli", cliente.EmailCliente);
                cmd.Parameters.AddWithValue("@TelefoneCli", cliente.TelefoneCliente);
                cmd.Parameters.AddWithValue("@SenhaCli", cliente.SenhaCliente);
                cmd.Parameters.AddWithValue("@NascCli", cliente.DataNascimentoCliente.ToString("yyyy-MM-dd"));
   
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void ExcluirCliente(int IdCliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbCliente where IdCliente = @IdCliente", conexao);
                cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Cliente ObterClientePorId(int IdCliente)
        {
            List<Cliente> clientes = new List<Cliente>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbCliente where IdCliente=@IdCliente", conexao);
                cmd.Parameters.AddWithValue("@IdCliente", IdCliente);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


                Cliente cliente = new Cliente();

                while (dr.Read())
                {

                    cliente.IdCliente = Convert.ToInt32(dr["IdCliente"]);
                    cliente.CpfCliente = Convert.ToChar(dr["CpfCli"]);
                    cliente.NomeCliente = Convert.ToString(dr["NomeCli"]);
                    cliente.EmailCliente = Convert.ToString(dr["EmailCli"]);
                    cliente.TelefoneCliente = Convert.ToString(dr["TelefoneCli"]);
                    cliente.SenhaCliente = Convert.ToString(dr["SenhaCli"]);
                    cliente.DataNascimentoCliente = DateOnly.FromDateTime(Convert.ToDateTime(dr["NascCli"]));

                }
                Console.WriteLine(cliente.NomeCliente);
                return cliente;

            }
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbCliente", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    clientes.Add
                        (
                            new Cliente
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                CpfCliente = Convert.ToChar(dr["CpfCli"]),
                                NomeCliente = Convert.ToString(dr["NomeCli"]),
                                EmailCliente = Convert.ToString(dr["EmailCli"]),
                                TelefoneCliente = Convert.ToString(dr["TelefoneCli"]),
                                SenhaCliente = Convert.ToString(dr["SenhaCli"]),
                                DataNascimentoCliente = DateOnly.FromDateTime(Convert.ToDateTime(dr["NascCli"]))
                            }

                        );
                }
                return clientes;
            }
        }
    }
}
