namespace IN_TEGRA.Models
{
    public class Pagamento
    {
       public int IdPagamento { get; set; }
       public int NF { get; set; }
       public double ValorPagamento { get; set; }
       public int TipoPagamento { get; set; }
       public bool PagamentoConfirmado { get; set; }
       public DateOnly DataPagamento { get; set; }
    }
}
