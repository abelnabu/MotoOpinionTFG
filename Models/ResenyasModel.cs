using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoOpinion.Models
{
    public class ResenyasModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Título es obligatorio")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo Texto es obligatorio")]
        public string? Texto { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        public DateTime? Fecha { get; set; }

        public string? ImagenBase64 { get; set; }

        public int ModeloMoto{ get; set; }

        public int Usuario{ get; set; }

        public string? UsuarioNombre { get; set; }
    }
}
