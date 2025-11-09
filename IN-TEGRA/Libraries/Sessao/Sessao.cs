namespace IN_TEGRA.Libraries.Sessao
{
    public class Sessao
    {
        IHttpContextAccessor _contexto;

        public Sessao(IHttpContextAccessor contexto)
        {
            _contexto = contexto;
        }

        public void Cadastrar(string Key, string valor)
        {
            _contexto.HttpContext.Session.SetString(Key, valor);
        }

        public string Consultar(string Key)
        {
            return _contexto.HttpContext.Session.GetString(Key);
        }   

        public bool Existe(string Key)
        {
           if(_contexto.HttpContext.Session.GetString(Key) != null)
           {
               return true;
           }
           return false;
        }

        public void RemoverTodos(string Key)
        {
            _contexto.HttpContext.Session.Clear();
        }

        public void Atualizar(string Key, string valor)
        {
            if (Existe(Key))
            {
                RemoverTodos(Key);
            }
            _contexto.HttpContext.Session.SetString(Key, valor);
        }
    }
}
