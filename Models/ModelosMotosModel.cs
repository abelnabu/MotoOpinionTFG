using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoOpinion.Models
{
    public class ModelosMotosModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Marca es obligatorio")]
        public int MarcaId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string? Nombre { get; set; }

        public int? Cilindrada { get; set; }
        public string? TipoMotor { get; set; }

        public decimal? Potencia { get; set; }

        public decimal? Peso { get; set; }
        public string? Tipo { get; set; }
        public int? Anyo { get; set; }
        public decimal? Precio { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenBase64 { get; set; }

        public string? MarcaNombre { get; set; }
    }
}
