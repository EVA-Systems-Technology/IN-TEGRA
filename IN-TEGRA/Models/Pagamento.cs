namespace IN_TEGRA.Models
{
    public class Pagamento
    {
       public int IdPagamento { get; set; }
       public int IdPedido { get; set; }
       public int IdCliente { get; set; }
       public double ValorPagamento { get; set; }
       public string TipoPagamento { get; set; }
       public DateTime DataHoraPagamento { get; set; }
    }
}
