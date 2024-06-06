using MotoOpinion.Models;
using System.Data.SqlClient;
using System.Data;
using MotoOpinion.Data;
using static MotoOpinion.Controllers.MarcasMotosController;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MotoOpinion.Data
{
    public class ModelosMotosData
    {

        public List<ModelosMotosModel> Listar()
        {

            var oLista = new List<ModelosMotosModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("modMotos_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new ModelosMotosModel()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            MarcaId = Convert.ToInt32(dr["MarcaId"]),
                            Nombre = dr["Nombre"].ToString(),
                            Cilindrada = Convert.ToInt32(dr["Cilindrada"]),
                            TipoMotor = dr["TipoMotor"].ToString(),
                            Potencia = Convert.ToDecimal(dr["Potencia"]),
                            Peso = Convert.ToDecimal(dr["Peso"]),
                            Tipo = dr["Tipo"].ToString(),
                            Anyo = Convert.ToInt32(dr["Año"]),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            Descripcion = dr["Descripcion"].ToString(),
                            ImagenBase64 = dr["ImagenBase64"].ToString(),
                            MarcaNombre = dr["NombreMarca"].ToString(),
                        });

                    }
                }
            }

            return oLista;
        }

        public ModelosMotosModel Obtener(string Nombre) //Los nombres sonn únicos
        {

            var modeloMoto = new ModelosMotosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("modMotos_Obtener", conexion);
                cmd.Parameters.AddWithValue("@Nombre", Nombre);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        modeloMoto.Id = Convert.ToInt32(dr["Id"]);
                        modeloMoto.MarcaId = Convert.ToInt32(dr["MarcaId"]);
                        modeloMoto.Nombre = dr["Nombre"].ToString();
                        modeloMoto.Cilindrada = Convert.ToInt32(dr["Cilindrada"]);
                        modeloMoto.TipoMotor = dr["TipoMotor"].ToString();
                        modeloMoto.Potencia = Convert.ToDecimal(dr["Potencia"]);
                        modeloMoto.Peso = Convert.ToDecimal(dr["Peso"]);
                        modeloMoto.Tipo = dr["Tipo"].ToString();
                        modeloMoto.Anyo = Convert.ToInt32(dr["Año"]);
                        modeloMoto.Precio = Convert.ToDecimal(dr["Precio"]);
                        modeloMoto.Descripcion = dr["Descripcion"].ToString();
                        modeloMoto.ImagenBase64 = dr["ImagenBase64"].ToString();
                    }
                }
            }

            return modeloMoto;
        }

        public bool Crear(ModelosMotosModel modeloMoto)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("modMotos_Crear", conexion);
                    cmd.Parameters.AddWithValue("@MarcaId", modeloMoto.MarcaId);
                    cmd.Parameters.AddWithValue("@Nombre", modeloMoto.Nombre);
                    cmd.Parameters.AddWithValue("@Cilindrada", modeloMoto.Cilindrada);
                    cmd.Parameters.AddWithValue("@TipoMotor", modeloMoto.TipoMotor);
                    cmd.Parameters.AddWithValue("@Potencia", modeloMoto.Potencia);
                    cmd.Parameters.AddWithValue("@Peso", modeloMoto.Peso);
                    cmd.Parameters.AddWithValue("@Tipo", modeloMoto.Tipo);
                    cmd.Parameters.AddWithValue("@Anyo", modeloMoto.Anyo);
                    cmd.Parameters.AddWithValue("@Precio", modeloMoto.Precio);
                    cmd.Parameters.AddWithValue("@Descripcion", modeloMoto.Descripcion);
                    cmd.Parameters.AddWithValue("@ImagenBase64", modeloMoto.ImagenBase64);
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

        public bool Existe(string nombre)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("modMotos_Existe", conexion);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
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


        public bool Editar(ModelosMotosModel modeloMoto)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("modMotos_Editar", conexion);
                    cmd.Parameters.AddWithValue("MarcaId", modeloMoto.MarcaId);
                    cmd.Parameters.AddWithValue("Nombre", modeloMoto.Nombre);
                    cmd.Parameters.AddWithValue("Cilindrada", modeloMoto.Cilindrada);
                    cmd.Parameters.AddWithValue("TipoMotor", modeloMoto.TipoMotor);
                    cmd.Parameters.AddWithValue("Potencia", modeloMoto.Potencia);
                    cmd.Parameters.AddWithValue("Peso", modeloMoto.Peso);
                    cmd.Parameters.AddWithValue("Tipo", modeloMoto.Tipo);
                    cmd.Parameters.AddWithValue("Anyo", modeloMoto.Anyo);
                    cmd.Parameters.AddWithValue("Precio", modeloMoto.Precio);
                    cmd.Parameters.AddWithValue("Descripcion", modeloMoto.Descripcion);
                    cmd.Parameters.AddWithValue("ImagenBase64", modeloMoto.ImagenBase64);
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
                    SqlCommand cmd = new SqlCommand("modMotos_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
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