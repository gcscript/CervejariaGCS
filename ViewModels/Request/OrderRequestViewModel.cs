using System.ComponentModel.DataAnnotations;

namespace CervejariaGCS.ViewModels.Request;

public class OrderRequestViewModel
{
    public ICollection<ProductAndQuantityRequest> ProductsAndQuantityRequest { get; set; }
}

public class ProductAndQuantityRequest
{
    [Required(ErrorMessage = "O produto é obrigatório")]
    public Guid Product { get; set; }

    [Required(ErrorMessage = "O quantidade é obrigatória")]
    [Range(1, 99, ErrorMessage = "A quantidade deve ser maior que 0 e menor que 99")]
    public byte Quantity { get; set; }
}
