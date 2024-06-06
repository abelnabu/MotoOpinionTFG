using MotoOpinion.Models;
using System.Data.SqlClient;
using System.Data;
using MotoOpinion.Data;

namespace MotoOpinion.Data
{
    public class SuscripcionesData
    {

        public List<SuscripcionesModel> Listar(int UsuarioMaestro)
        {

            var oLista = new List<SuscripcionesModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("suscripciones_Listar", conexion);
                cmd.Parameters.AddWithValue("@UsuarioMaestro", UsuarioMaestro);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new SuscripcionesModel()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            UsuarioMaestro = Convert.ToInt32(dr["UsuarioMaestro"]),
                            UsuarioSuscriptor = Convert.ToInt32(dr["UsuarioSuscriptor"]),
                        });

                    }
                }
            }

            return oLista;
        }

        public bool Crear(SuscripcionesModel Suscripcion)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("suscripciones_Crear", conexion);
                    cmd.Parameters.AddWithValue("@UsuarioMaestro", Suscripcion.UsuarioMaestro);
                    cmd.Parameters.AddWithValue("@UsuarioSuscriptor", Suscripcion.UsuarioSuscriptor);
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


        public List<SuscripcionesModel> EstaSuscrito(SuscripcionesModel Suscripcion)
        {

            var oLista = new List<SuscripcionesModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("suscripciones_suscrito", conexion);
                cmd.Parameters.AddWithValue("@UsuarioMaestro", Suscripcion.UsuarioMaestro);
                cmd.Parameters.AddWithValue("@UsuarioSuscriptor", Suscripcion.UsuarioSuscriptor);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new SuscripcionesModel()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            UsuarioMaestro = Convert.ToInt32(dr["UsuarioMaestro"]),
                            UsuarioSuscriptor = Convert.ToInt32(dr["UsuarioSuscriptor"]),
                        });

                    }
                }
            }

            return oLista;
        }

        public bool Eliminar(SuscripcionesModel Suscripcion)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("suscripciones_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("@UsuarioMaestro", Suscripcion.UsuarioMaestro);
                    cmd.Parameters.AddWithValue("@UsuarioSuscriptor", Suscripcion.UsuarioSuscriptor);
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