using System.ComponentModel.DataAnnotations;

namespace IN_TEGRA.Models
{
    public class Cliente
    {
        [Display(Name = "Código")]
        public int IdCliente { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O campo nome é obrigatorio")]
        public string NomeCliente { get; set; }

        [Display(Name = "Email Completo")]
        [Required(ErrorMessage = "O campo Email é obrigatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string EmailCliente { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatorio")]
        [DataType(DataType.Password, ErrorMessage = "Senha inválida.")]
        public string SenhaCliente { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo CPF é obrigatorio")]
        public decimal CpfCliente { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O campo Telefone é obrigatorio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Telefone em formato inválido.")]
        public string TelefoneCliente { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatorio")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido.")]
        public DateOnly DataNascimentoCliente { get; set; }

    }
}
