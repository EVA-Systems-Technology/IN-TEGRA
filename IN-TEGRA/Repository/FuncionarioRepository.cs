using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

namespace IN_TEGRA.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly string _conexaoMySQL;

        public FuncionarioRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void atualizarFuncionario(Funcionario funcionario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Update tbFuncionario set NomeFunc=@NomeFunc, EmailFunc=@EmailFunc, CpfFunc=@CpfFunc, TipoFunc=@TipoFunc where IdFuncionario=@IdFuncionario;", conexao);

                cmd.Parameters.AddWithValue("@IdFuncionario", funcionario.IdFuncionario);
                cmd.Parameters.AddWithValue("@NomeFunc", funcionario.NomeFuncionario);
                cmd.Parameters.AddWithValue("@EmailFunc", funcionario.EmailFuncionario);
                cmd.Parameters.AddWithValue("@CpfFunc", funcionario.CpfFuncionario);
                cmd.Parameters.AddWithValue("@TipoFunc", funcionario.TipoFunc);
                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public void atualizarSenhaFuncionario(Funcionario funcionario)
        {
            throw new NotImplementedException();
        }

        public void cadastrarFuncionario(Funcionario funcionario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tbFuncionario (NomeFunc, EmailFunc, SenhaFunc, CpfFunc, TipoFunc) values (@NomeFunc, @EmailFunc, @SenhaFunc, @CpfFunc, @TipoFunc)", conexao);
                cmd.Parameters.AddWithValue("@NomeFunc", funcionario.NomeFuncionario);
                cmd.Parameters.AddWithValue("@EmailFunc", funcionario.EmailFuncionario);
                cmd.Parameters.AddWithValue("@SenhaFunc", funcionario.SenhaFuncionario);
                cmd.Parameters.AddWithValue("@CpfFunc", funcionario.CpfFuncionario);
                cmd.Parameters.AddWithValue("@TipoFunc", funcionario.TipoFunc);
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void excluirFuncionario(int IdFuncionario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbFuncionario where IdFuncionario=@IdFuncionario", conexao);
                cmd.Parameters.AddWithValue("@IdFuncionario", IdFuncionario);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }


        public List<Funcionario> obterFuncionarioPorEmail(string Email)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbFuncionario where EmailFunc=@EmailFunc", conexao);

                cmd.Parameters.AddWithValue("@EmailFunc", Email);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows) 
                {
                  funcionarios.Add(
                      new Funcionario
                    {
                        IdFuncionario = Convert.ToInt32(dr["IdFuncionario"]),
                        NomeFuncionario = Convert.ToString(dr["NomeFunc"]),
                        EmailFuncionario = Convert.ToString(dr["EmailFunc"]),
                        SenhaFuncionario = Convert.ToString(dr["SenhaFunc"]),
                        CpfFuncionario = Convert.ToString(dr["CpfFunc"]),
                        TipoFunc = Convert.ToString(dr["TipoFunc"])
                      });
                }
                return funcionarios;
            }
        }

        public Funcionario obterFuncionarioPorId(int IdFuncionario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbFuncionario where IdFuncionario=@IdFuncionario", conexao);
                cmd.Parameters.AddWithValue("@IdFuncionario", IdFuncionario);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                Funcionario funcionario = new Funcionario();

                while (dr.Read()) 
                {
                    funcionario.IdFuncionario = Convert.ToInt32(dr["IdFuncionario"]);
                    funcionario.NomeFuncionario = dr["NomeFunc"].ToString();
                    funcionario.EmailFuncionario = dr["EmailFunc"].ToString();
                    funcionario.SenhaFuncionario = dr["SenhaFunc"].ToString();
                    funcionario.CpfFuncionario = dr["CpfFunc"].ToString();
                    funcionario.TipoFunc = dr["TipoFunc"].ToString();

                }
                return funcionario;

            }
        }

        public IEnumerable<Funcionario> obterTodosFuncionarios()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbFuncionario", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);
                conexao.Close();

                foreach(DataRow dr in dt.Rows) 
                {
                    funcionarios.Add(new Funcionario
                    {
                        IdFuncionario = Convert.ToInt32(dr["IdFuncionario"]),
                        NomeFuncionario = Convert.ToString(dr["NomeFunc"]),
                        EmailFuncionario = Convert.ToString(dr["EmailFunc"]),
                        SenhaFuncionario = Convert.ToString(dr["SenhaFunc"]),
                        CpfFuncionario = Convert.ToString(dr["CpfFunc"]),
                        TipoFunc = Convert.ToString(dr["TipoFunc"])
                    });

                }
                return funcionarios;


            }
        }
    }
}
