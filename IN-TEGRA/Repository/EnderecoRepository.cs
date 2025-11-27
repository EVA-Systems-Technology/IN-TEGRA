using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

namespace IN_TEGRA.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly string _conexaoMySQL;

        public EnderecoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void CadastrarEndereco(Endereco endereco)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Insert into TbEndereco(CEP, Estado, Cidade, Bairro, LogradouroEndereco, Numero, Complemento) values(@CEP, @Estado, @Cidade, @Bairro, @LogradouroEndereco, @Numero, @Complemento)", conexao);

                cmd.Parameters.AddWithValue("@CEP", endereco.CEP);
                cmd.Parameters.AddWithValue("@Estado", endereco.Estado);
                cmd.Parameters.AddWithValue("@Cidade", endereco.Cidade);
                cmd.Parameters.AddWithValue("@Bairro", endereco.Bairro);
                cmd.Parameters.AddWithValue("@LogradouroEndereco", endereco.Logradouro);
                cmd.Parameters.AddWithValue("@Numero", endereco.Numero);
                cmd.Parameters.AddWithValue("@Complemento", endereco.Complemento);

                cmd.ExecuteNonQuery();

                //pega o id gerado
                endereco.IdEndereco = (int)cmd.LastInsertedId;
            }
        }


        public Endereco ObterEnderecoPorId(int IdEndereco)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Select * from tbEndereco where IdEndereco=@IdEndereco", conexao);
                cmd.Parameters.AddWithValue("@IdEndereco", IdEndereco);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


                Endereco endereco = new Endereco();
                while (dr.Read())
                {
                    endereco.IdEndereco = Convert.ToInt32(dr["IdEndereco"]);
                    endereco.CEP = dr["CEP"].ToString();
                    endereco.Estado = dr["Estado"].ToString();
                    endereco.Cidade = dr["Cidade"].ToString();
                    endereco.Bairro = dr["Bairro"].ToString();
                    endereco.Logradouro = dr["LogradouroEndereco"].ToString();
                    endereco.Complemento = dr["Complemento"].ToString();
                    endereco.Numero = Convert.ToInt32(dr["Numero"]);
                }
                return endereco;
            }
        }
    }
}
