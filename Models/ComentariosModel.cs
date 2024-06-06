using System.ComponentModel.DataAnnotations;

namespace MotoOpinion.Models
{
    public class ComentariosModel
    {
        [Key]
        public int Id { get; set; }

        public int? Usuario { get; set; }
        public string? Texto { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Puntuacion { get; set; }
        public int? IdResenya { get; set; }

        public string? NombreUsuario { get; set; }

    }
}
