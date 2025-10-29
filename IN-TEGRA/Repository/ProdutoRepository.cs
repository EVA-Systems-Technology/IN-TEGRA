using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

namespace IN_TEGRA.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string _conexaoMySQL;

        public ProdutoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void AtualizarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Update tbProduto set NomeProd=@NomeProd, DescProd=@DescProd, ImgProd=@ImgProd, PrecoProd=@PrecoProd, QtdProd=@QtdProd where IdProd=@IdProd;", conexao);

                cmd.Parameters.AddWithValue("@IdProd", produto.IdProd);
                cmd.Parameters.AddWithValue("@NomeProd", produto.NomeProduto);
                cmd.Parameters.AddWithValue("@DescProd", produto.DescricaoProduto);
                cmd.Parameters.AddWithValue("ImgProd", produto.ImagemProduto);
                cmd.Parameters.AddWithValue("@PrecoProd", produto.PrecoProduto);
                cmd.Parameters.AddWithValue("@QtdProd", produto.QuantidadeProduto);

                cmd.ExecuteNonQuery();
                conexao.Close();

            }

        }

        public void CadastrarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbProduto(NomeProd, DescProd, ImgProd, PrecoProd, QtdProd) values(@NomeProd, @DescProd, @ImgProd, @PrecoProd, @QtdProd)", conexao);

                cmd.Parameters.AddWithValue("@NomeProd", produto.NomeProduto);
                cmd.Parameters.AddWithValue("@DescProd", produto.DescricaoProduto);
                cmd.Parameters.AddWithValue("ImgProd", produto.ImagemProduto);
                cmd.Parameters.AddWithValue("@PrecoProd", produto.PrecoProduto);
                cmd.Parameters.AddWithValue("@QtdProd", produto.QuantidadeProduto);

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void ExcluirProduto(int IdProd)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL)) 
            {   
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbProduto where IdProd = @IdProd", conexao);
                cmd.Parameters.AddWithValue("@IdProd", IdProd);
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Produto> ObterTodosProdutos()
        {
            List<Produto> produtos = new List<Produto>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbProduto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    produtos.Add(
                            new Produto
                            {
                                IdProd = Convert.ToInt32(dr["IdProd"]),
                                NomeProduto = Convert.ToString(dr["NomeProd"]),
                                DescricaoProduto = Convert.ToString(dr["DescProd"]),
                                ImagemProduto = Convert.ToString(dr["ImgProd"]),
                                PrecoProduto = Convert.ToInt32(dr["PrecoProd"]),
                                QuantidadeProduto = Convert.ToInt32(dr["QtdProd"])
                            }
                    );
                }

                return produtos;
            }
        }

        Produto IProdutoRepository.ObterProdutoPorId(int IdProd)
        {
            List<Produto> produtos = new List<Produto>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbProduto where IdProd=@IdProd", conexao);
                cmd.Parameters.AddWithValue("@IdProd", IdProd);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


                Produto produto = new Produto();

                while (dr.Read())
                {
                    produto.IdProd = Convert.ToInt32(dr["IdProd"]);
                    produto.NomeProduto = Convert.ToString(dr["NomeProd"]);
                    produto.DescricaoProduto = Convert.ToString(dr["DescProd"]);
                    produto.ImagemProduto = Convert.ToString(dr["ImgProd"]);
                    produto.PrecoProduto = Convert.ToInt32(dr["PrecoProd"]);
                    produto.QuantidadeProduto = Convert.ToInt32(dr["QtdProd"]);



                }

                return produto;
            }

        }       
    } 

}
