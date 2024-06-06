using MotoOpinion.Models;
using System.Data.SqlClient;
using System.Data;
using MotoOpinion.Data;

namespace MotoOpinion.Data
{
    public class UsuariosData
    {

        public List<UsuariosModel> Listar()
        {

            var oLista = new List<UsuariosModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("usu_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new UsuariosModel()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString(),
                            Email = dr["Email"].ToString(),
                            Clave = dr["Clave"].ToString(),
                            Rol = dr["Rol"].ToString(),

                        });

                    }
                }
            }

            return oLista;
        }

        public UsuariosModel Obtener(String NombreUsuario) //Los nombres sonn únicos
        {

            var usu = new UsuariosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("usu_Obtener", conexion);
                cmd.Parameters.AddWithValue("@Usuario", NombreUsuario);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        usu.Id = Convert.ToInt32(dr["Id"]);
                        usu.Nombre = dr["Nombre"].ToString();
                        usu.Email = dr["Email"].ToString();
                        usu.Clave = dr["Clave"].ToString();
                        usu.Rol = dr["Rol"].ToString();
                    }
                }
            }

            return usu;
        }
        public UsuariosModel ObtenerPorId(int IdUsuario) //Los nombres sonn únicos
        {

            var usu = new UsuariosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("usu_ObtenerPorId", conexion);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        usu.Id = Convert.ToInt32(dr["Id"]);
                        usu.Nombre = dr["Nombre"].ToString();
                        usu.Email = dr["Email"].ToString();
                        usu.Clave = dr["Clave"].ToString();
                        usu.Rol = dr["Rol"].ToString();
                    }
                }
            }

            return usu;
        }

        public bool Crear(UsuariosModel usu)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("usu_Crear", conexion);
                    cmd.Parameters.AddWithValue("Nombre", usu.Nombre);
                    cmd.Parameters.AddWithValue("Email", usu.Email);
                    cmd.Parameters.AddWithValue("Clave", usu.Clave);
                    cmd.Parameters.AddWithValue("Rol", usu.Rol);
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

        public bool ComprobarUsuario(String usuario, String password)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("usu_VerificarUsuarioContraseña", conexion);
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.Add("@IsValid", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    rpta = Convert.ToBoolean(cmd.Parameters["@IsValid"].Value);
                }
                

            }
            catch (Exception e)
            {

                string error = e.Message;
                rpta = false;
            }



            return rpta;
        }

        public bool Existe(String usuarioNombre)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("usu_Existe", conexion);
                    cmd.Parameters.AddWithValue("@Usuario", usuarioNombre);
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


        public bool Editar(UsuariosModel usu)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("usu_Editar", conexion);
                    cmd.Parameters.AddWithValue("Nombre", usu.Nombre);
                    cmd.Parameters.AddWithValue("Email", usu.Email);
                    cmd.Parameters.AddWithValue("Clave", usu.Clave);
                    cmd.Parameters.AddWithValue("Rol", usu.Rol);
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

        public bool Eliminar(string nombre)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("usu_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("Nombre", nombre);
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