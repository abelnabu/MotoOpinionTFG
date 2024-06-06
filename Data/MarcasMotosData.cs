using MotoOpinion.Models;
using System.Data.SqlClient;
using System.Data;
using MotoOpinion.Data;

namespace MotoOpinion.Data
{
    public class MarcasMotosData
    {

        public List<MarcasMotosModel> Listar()
        {

            var oLista = new List<MarcasMotosModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("MarcasMotos_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new MarcasMotosModel()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString()

                        });

                    }
                }
            }

            return oLista;
        }

        public MarcasMotosModel Obtener(String NombreMarca) //Los nombres sonn únicos
        {

            var usu = new MarcasMotosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("MarcasMotos_Obtener", conexion);
                cmd.Parameters.AddWithValue("@NombreMarca", NombreMarca);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        usu.Id = Convert.ToInt32(dr["Id"]);
                        usu.Nombre = dr["Nombre"].ToString();
                    }
                }
            }

            return usu;
        }

        public MarcasMotosModel ObtenerId(int IdMarca) 
        {

            var usu = new MarcasMotosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("MarcasMotos_ObtenerId", conexion);
                cmd.Parameters.AddWithValue("@IdMarca", IdMarca);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        usu.Id = Convert.ToInt32(dr["Id"]);
                        usu.Nombre = dr["Nombre"].ToString();
                    }
                }
            }

            return usu;
        }
        

        public bool Crear(MarcasMotosModel marca)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("MarcasMotos_Crear", conexion);
                    cmd.Parameters.AddWithValue("Nombre", marca.Nombre);
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


        public bool Existe(String marca)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("MarcasMotos_Existe", conexion);
                    cmd.Parameters.AddWithValue("@Marca", marca);
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


        public bool Eliminar(string marca)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("MarcasMotos_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("@Nombre", marca);
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