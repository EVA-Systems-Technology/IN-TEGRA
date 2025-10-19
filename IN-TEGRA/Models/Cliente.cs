namespace IN_TEGRA.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public string SenhaCliente { get; set; }
        public int CpfCliente { get; set; }
        public int TelefoneCliente { get; set; }
        public DateOnly DataNascimentoCliente { get; set; }
    }
}
