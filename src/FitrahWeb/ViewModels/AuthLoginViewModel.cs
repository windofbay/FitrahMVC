using System.ComponentModel.DataAnnotations;

namespace FitrahWeb.ViewModels;

public class AuthLoginViewModel
{
    [Required(ErrorMessage ="Username wajib diisi!")]
    public string Username { get; set; } = null!;
    [Required(ErrorMessage ="Password wajib diisi!")]
    public string Password { get; set; } = null!;
}
