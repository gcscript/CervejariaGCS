using CervejariaGCS.Models;
using System.ComponentModel.DataAnnotations;

namespace CervejariaGCS.ViewModels.Request;

public class CashbackRequestViewModel
{
    [Required(ErrorMessage = "O id do produto é obrigatório")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "O dia da semana é obrigatório")]
    [Range(0, 6, ErrorMessage = "O dia da semana deve estar entre 0 e 6")]
    public short DayOfWeek { get; set; }

    [Required(ErrorMessage = "A porcentagem é obrigatória")]
    [Range(1, 100, ErrorMessage = "A porcentagem deve estar entre 1 e 100")]
    public short Percent { get; set; }
}
