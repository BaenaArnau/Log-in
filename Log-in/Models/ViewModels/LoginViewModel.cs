using System.ComponentModel.DataAnnotations;

namespace Log_in.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
