using System.ComponentModel.DataAnnotations;

namespace AMochika.Core.Entities.Auth;
    // Modelo para recibir las credenciales de login
    public class Login
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Username { get; set; } 

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
    }

    // Modelo para devolver el token generado
    public class TokenResponse
    {
        public string Token { get; set; }
    }