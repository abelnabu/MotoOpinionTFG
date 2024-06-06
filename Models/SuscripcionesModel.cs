using System.ComponentModel.DataAnnotations;

namespace MotoOpinion.Models
{
    public class SuscripcionesModel
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioSuscriptor { get; set; }
        public int UsuarioMaestro { get; set; }


    }
}
