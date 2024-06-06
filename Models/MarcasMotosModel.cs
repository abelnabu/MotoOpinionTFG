using System.ComponentModel.DataAnnotations;

namespace MotoOpinion.Models
{
    public class MarcasMotosModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string? Nombre { get; set; }

        
    }
}
