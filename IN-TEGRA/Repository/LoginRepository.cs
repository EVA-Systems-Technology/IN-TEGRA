using IN_TEGRA.Libraries.Login;
using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Data;

namespace IN_TEGRA.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _conexaoMySQL;

        public LoginRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public Cliente LoginComum(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbCliente where EmailCli=@EmailCli and SenhaCli=@SenhaCli", conexao);

                cmd.Parameters.AddWithValue("@EmailCli", Email);
                cmd.Parameters.AddWithValue("@SenhaCli", Senha);

                MySqlDataReader dr = cmd.ExecuteReader();

                Cliente cliente = new Cliente();

                while (dr.Read())
                {
                    cliente.IdCli = Convert.ToInt32(dr["IdCli"]);
                    cliente.NomeCliente = Convert.ToString(dr["NomeCli"]);
                    cliente.EmailCliente = Convert.ToString(dr["EmailCli"]);
                    cliente.SenhaCliente = Convert.ToString(dr["SenhaCli"]);
                    cliente.CpfCliente = Convert.ToDouble(dr["CpfCli"]);
                    cliente.TelefoneCliente = Convert.ToString(dr["TelefoneCli"]);
                    cliente.DataNascimentoCliente = DateOnly.FromDateTime(Convert.ToDateTime(dr["NascCli"]));

                }

                dr.Close();

                if (cliente.IdCli != 0)
                {

                    MySqlCommand cmd2 = new MySqlCommand("insert into tblogin (IdCli, TipoLogin) values (@IdCli, @TipoLogin)", conexao);

                    cmd2.Parameters.AddWithValue("@IdCli", cliente.IdCli);
                    cmd2.Parameters.AddWithValue("@TipoLogin", "Cliente");
                    cmd2.ExecuteNonQuery();

                }
                conexao.Close();

                return cliente;
            }
        }

        public Funcionario LoginFuncionario(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbFuncionario where EmailFunc=@EmailFunc and SenhaFunc=@SenhaFunc", conexao);

                cmd.Parameters.AddWithValue("@EmailFunc", Email);
                cmd.Parameters.AddWithValue("@SenhaFunc", Senha);

                MySqlDataReader dr = cmd.ExecuteReader();

                Funcionario funcionario = new Funcionario();

                while (dr.Read())
                {
                    funcionario.IdFuncionario = Convert.ToInt32(dr["IdFunc"]);
                    funcionario.NomeFuncionario = dr["NomeFunc"].ToString();
                    funcionario.EmailFuncionario = dr["EmailFunc"].ToString();
                    funcionario.SenhaFuncionario = dr["SenhaFunc"].ToString();
                    funcionario.CpfFuncionario = dr["CpfFunc"].ToString();
                    funcionario.TipoFunc = dr["TipoFunc"].ToString();
                }

                dr.Close();

                if (funcionario != null) 
                {

                    MySqlCommand cmd2 = new MySqlCommand("insert into tblogin (IdFunc, TipoLogin) values (@IdFunc, @TipoLogin)", conexao);

                    cmd2.Parameters.AddWithValue("@IdFunc", funcionario.IdFuncionario);
                    cmd2.Parameters.AddWithValue("@TipoLogin", "Funcionario");
               
                    cmd2.ExecuteNonQuery();

                
                }


                conexao.Close();

                return funcionario;
            }
        }

        public Login ObterUltimoLoginCliente(int IdCliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbLogin where IdCli=@IdCli ORDER BY DataLogin DESC Limit 1", conexao);
                cmd.Parameters.AddWithValue("@IdCli", IdCliente);

                MySqlDataReader dr = cmd.ExecuteReader();

                Login login = new Login();

                while (dr.Read())
                {
                    login.IdLogin = Convert.ToInt32(dr["IdLogin"]);
                    login.IdCliente = dr["IdCli"] != DBNull.Value ? Convert.ToInt32(dr["IdCli"]) : 0;
                    login.IdFuncionario = dr["IdFunc"] != DBNull.Value ? Convert.ToInt32(dr["IdFunc"]) : 0;
                    login.TipoLogin = dr["TipoLogin"].ToString();
                    login.DataLogin = Convert.ToDateTime(dr["DataLogin"]);
                }
                dr.Close();
                conexao.Close();

                return login;
            }
        }
    }
}
