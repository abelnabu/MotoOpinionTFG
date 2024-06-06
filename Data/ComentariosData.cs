using MotoOpinion.Models;
using System.Data.SqlClient;
using System.Data;
using MotoOpinion.Data;

namespace MotoOpinion.Data
{
    public class ComentariosData
    {

        public List<ComentariosModel> ListarComentariosResenya(int IdResenya)
        {

            var oLista = new List<ComentariosModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Comentarios_Listar", conexion);
                cmd.Parameters.AddWithValue("@IdResenya", IdResenya);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new ComentariosModel()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Usuario = Convert.ToInt32(dr["Usuario"]),
                            Texto = dr["Texto"].ToString(),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            Puntuacion = Convert.ToInt32(dr["Puntuacion"]),
                            NombreUsuario = dr["NombreUsuario"].ToString()
                        }) ;

                    }
                }
            }

            return oLista;
        }

        public bool Crear(ComentariosModel Comentario)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Comentarios_Crear", conexion);
                    cmd.Parameters.AddWithValue("Usuario", Comentario.Usuario);
                    cmd.Parameters.AddWithValue("Texto", Comentario.Texto);
                    cmd.Parameters.AddWithValue("Fecha", Comentario.Fecha);
                    cmd.Parameters.AddWithValue("Puntuacion", Comentario.Puntuacion);
                    cmd.Parameters.AddWithValue("IdResenya", Comentario.IdResenya);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {

                string error = e.Message;
                rpta = false;
            }



            return rpta;
        }

        public bool Eliminar(int IdResenya)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Comentarios_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("@IdResenya", IdResenya);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;


            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }


    }
}