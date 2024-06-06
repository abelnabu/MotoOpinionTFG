using MotoOpinion.Models;
using System.Data.SqlClient;
using System.Data;
using MotoOpinion.Data;
using static MotoOpinion.Controllers.MarcasMotosController;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MotoOpinion.Data
{
    public class ResenyasData
    {

        public List<ResenyasModel> Listar()
        {

            var oLista = new List<ResenyasModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("resenyas_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new ResenyasModel()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Titulo = dr["Titulo"].ToString(),
                            Texto = dr["Texto"].ToString(),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            ImagenBase64 = dr["ImagenBase64"].ToString(),
                            Usuario = Convert.ToInt32(dr["Usuario"]),
                            ModeloMoto = Convert.ToInt32(dr["ModeloMoto"]),
                            UsuarioNombre = dr["UsuarioNombre"].ToString()
                        });

                    }
                }
            }

            return oLista;
        }

        public ResenyasModel Obtener(int Id)
        {

            var modeloResenya = new ResenyasModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("resenyas_Obtener", conexion);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        modeloResenya.Id = Convert.ToInt32(dr["Id"]);
                        modeloResenya.Titulo = dr["Titulo"].ToString();
                        modeloResenya.UsuarioNombre = dr["UsuarioNombre"].ToString();
                        modeloResenya.Texto = dr["Texto"].ToString();
                        modeloResenya.ImagenBase64 = dr["ImagenBase64"].ToString();
                        modeloResenya.Usuario = Convert.ToInt32(dr["Usuario"]);
                        modeloResenya.ModeloMoto = Convert.ToInt32(dr["ModeloMoto"]);
                        modeloResenya.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    }
                }
            }

            return modeloResenya;
        }

        public bool Crear(ResenyasModel modeloResenya)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("resenyas_Crear", conexion);
                    cmd.Parameters.AddWithValue("@ModeloMoto", modeloResenya.ModeloMoto);
                    cmd.Parameters.AddWithValue("@Titulo", modeloResenya.Titulo);
                    cmd.Parameters.AddWithValue("@Texto", modeloResenya.Texto);
                    cmd.Parameters.AddWithValue("@Fecha", modeloResenya.Fecha);
                    cmd.Parameters.AddWithValue("@ImagenBase64", modeloResenya.ImagenBase64);
                    cmd.Parameters.AddWithValue("@Usuario", modeloResenya.Usuario);

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

        public bool Existe(int Id)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("resenyas_Existe", conexion);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.Add("@Existe", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    rpta = Convert.ToBoolean(cmd.Parameters["@Existe"].Value);
                }


            }
            catch (Exception e)
            {

                string error = e.Message;
                rpta = false;
            }



            return rpta;
        }


        public bool Editar(ResenyasModel modeloResenya)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("resenyas_Editar", conexion);
                    cmd.Parameters.AddWithValue("@ModeloMoto", modeloResenya.ModeloMoto);
                    cmd.Parameters.AddWithValue("@Titulo", modeloResenya.Titulo);
                    cmd.Parameters.AddWithValue("@Texto", modeloResenya.Texto);
                    cmd.Parameters.AddWithValue("@Fecha", modeloResenya.Fecha);
                    cmd.Parameters.AddWithValue("@ImagenBase64", modeloResenya.ImagenBase64);
                    cmd.Parameters.AddWithValue("@Usuario", modeloResenya.Usuario);
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

        public bool Eliminar(int Id)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("resenyas_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("@Id", Id);
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