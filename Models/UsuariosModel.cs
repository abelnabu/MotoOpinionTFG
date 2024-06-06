using System.ComponentModel.DataAnnotations;

namespace MotoOpinion.Models
{
    public class UsuariosModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El campo Clave es obligatorio")]
        public string? Clave { get; set; }

        [Required(ErrorMessage = "El campo Rol es obligatorio")]
        public string? Rol { get; set; }
    }
}
