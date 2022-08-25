using System.ComponentModel.DataAnnotations;

namespace CervejariaGCS.ViewModels.Request;

public class ProductRequestViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo deve conter de 3 a 40 caracteres")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O slug é obrigatório")]
    public string Slug { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório")]
    [Range(0.1, 999999, ErrorMessage = "O preço deve ser maior que 0 e menor que 1.000.000")]
    public decimal Price { get; set; }
}
