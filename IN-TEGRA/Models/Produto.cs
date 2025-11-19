using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace IN_TEGRA.Models
{
    public class Produto
    {
        [Display(Name = "Código")]
        [ValidateNever]
        public int IdProd { get; set; }

        [Display(Name = "Nome do Produto")]
        [Required(ErrorMessage = "O campo nome é obrigatorio")]
        public string NomeProduto { get; set; }

        [Display(Name = "Preço do Produto")]
        [Required(ErrorMessage = "O campo preço é obrigatorio")]
        public decimal PrecoProduto { get; set; }

        [Display(Name = "Descrição do Produto")]
        [Required(ErrorMessage = "O campo descrição é obrigatorio")]
        public string DescricaoProduto { get; set; }

        [Display(Name = "Quantidade de Produtos")]
        [Required(ErrorMessage = "O campo quantidade é obrigatorio")]
        public int QuantidadeProduto { get; set; }

        [Display(Name = "Imagem do Produto")]
        [Required(ErrorMessage = "O campo imagem é obrigatorio")]
        public string ImagemProduto { get; set; }

        [Display(Name = "Categoria do Produto")]
        [Required(ErrorMessage = "O campo categoria é obrigatorio")]
        public string CategoriaProduto { get; set; }

    }
}
