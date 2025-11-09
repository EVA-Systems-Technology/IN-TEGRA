using IN_TEGRA.Models;
using Newtonsoft.Json;

namespace IN_TEGRA.Libraries.Sessao.Login
{
    public class LoginFuncionario
    {
        private string Key = "Login.Funcionario";
        private Sessao _sessao;

        public LoginFuncionario(Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Funcionario funcionario)
        {
            string funcionarioJSONString = JsonConvert.SerializeObject(funcionario);
            _sessao.Cadastrar(Key, funcionarioJSONString);

        }

        public Funcionario GetFuncionario()
        {
            if (_sessao.Existe(Key))
            {
                string funcionarioJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Funcionario>(funcionarioJSONString);
            }
            else
            {
                return null;
            }
        }
        public void Logout()
        {
            _sessao.RemoverTodos(Key);

        }
    }
}
