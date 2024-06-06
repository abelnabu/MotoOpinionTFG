using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using MotoOpinion.Data;
using MotoOpinion.Models;
using System.Diagnostics;
using System.Text.Json;
using System.IO;
using System.Security.Cryptography;
using System.Buffers.Text;

namespace MotoOpinion.Controllers
{
    public class ModelosMotosController : Controller
    {
        public IActionResult Index(String error,String procesoRealizado, int paginacion)
        {
            ViewData["Title"] = "Modelos Motos";
            ModelosMotosData ModelosMotosData = new ModelosMotosData();
            List<ModelosMotosModel> listaModelosMotos = ModelosMotosData.Listar();
            
            MarcasMotosData MarcasMotosData = new MarcasMotosData();
            List<MarcasMotosModel> listaMarcasMotos = MarcasMotosData.Listar();
            ViewBag.Marcas = listaMarcasMotos;

            var paginas = listaModelosMotos.Count() / 10;
            var restoPaginas = listaModelosMotos.Count() % 10;

            if (restoPaginas > 0)
            {
                paginas++;
            }

            ViewBag.numeroPaginas = paginas;

            int registroInicial = 0;
            if (paginacion == 0) {
                ViewData["paginaActiva"] = "0";   
            }else
            {
                registroInicial = 10 * paginacion;
                ViewData["paginaActiva"] = paginacion.ToString();
            }

            List<ModelosMotosModel> listaModelosMotosPaginada = new List<ModelosMotosModel>();
            for (int i = registroInicial; i <= registroInicial+10; i++)
            {
                if (listaModelosMotos.Count() > i)
                {
                    listaModelosMotosPaginada.Add(listaModelosMotos[i]);
                }
            }

            ViewBag.ModelosMotos = listaModelosMotosPaginada;

            if (error != null && error != "") {
                ViewData["Error"] = error;
            }
            if (procesoRealizado != null && procesoRealizado != "")
            {
                ViewData["ProcesoRealizado"] = procesoRealizado;
            }
            return View();
        }


        [HttpPost]
        public JsonResult leerModelo(Mod mod )
        {
            ModelosMotosData ModelosMotosData = new ModelosMotosData();
            var ModeloMoto = ModelosMotosData.Obtener(mod.Nombre);

            MarcasMotosData marcasMotosData = new MarcasMotosData();
            MarcasMotosModel MarcaObj = marcasMotosData.ObtenerId(ModeloMoto.MarcaId);

            ModeloMoto.MarcaNombre = MarcaObj.Nombre;
            string jsonString = JsonSerializer.Serialize(ModeloMoto);
            return Json(jsonString);
        }

        [HttpPost]
        public IActionResult crear(string Nombre, string MarcaNombre, int Cilindrada, string TipoMotor, decimal Potencia, decimal Peso, string Tipo, int Anyo, decimal Precio, string Descripcion,string ImagenBase64) 
        {
            var errorMnsj = "";
            var proceso = "";

            MarcasMotosData marcasMotosData = new MarcasMotosData();
            MarcasMotosModel marcaObj = marcasMotosData.Obtener(MarcaNombre);

            ModelosMotosModel ModeloMotoCrear = new ModelosMotosModel();
            ModeloMotoCrear.Nombre = Nombre;
            ModeloMotoCrear.MarcaId = marcaObj.Id;
            ModeloMotoCrear.Cilindrada = Cilindrada;
            ModeloMotoCrear.TipoMotor = TipoMotor;
            ModeloMotoCrear.Potencia = Potencia;
            ModeloMotoCrear.Peso = Peso;
            ModeloMotoCrear.Tipo = Tipo;
            ModeloMotoCrear.Anyo = Anyo;
            ModeloMotoCrear.Precio = Precio;
            ModeloMotoCrear.Descripcion = Descripcion;
            ModeloMotoCrear.ImagenBase64 = ImagenBase64;

            if (TipoMotor == null) { ModeloMotoCrear.TipoMotor = ""; }
            if (Tipo == null) { ModeloMotoCrear.Tipo = ""; }
            if (Descripcion == null) { ModeloMotoCrear.Descripcion = ""; }
            if (ImagenBase64 == null) { ModeloMotoCrear.ImagenBase64 = ""; }

            ModelosMotosData ModelosMotosData = new ModelosMotosData();
            if (!ModelosMotosData.Existe(Nombre))
            {
                bool creardo = ModelosMotosData.Crear(ModeloMotoCrear);
                if (!creardo)
                {
                    errorMnsj = "Error al crear el Modelo";
                }
                else {
                    proceso = "Modelo creado correctamente";
                }
            }
            else
            {
                errorMnsj = "Error, el Modelo ya existe";
            }
            return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });
           
        }

        [HttpPost]
        public IActionResult editar(string Nombre, string MarcaNombre, int Cilindrada, string TipoMotor, decimal Potencia, decimal Peso, string Tipo, int Anyo, decimal Precio, string Descripcion, string ImagenBase64)
        {
            var proceso = "";
            var errorMnsj = "";

            MarcasMotosData marcasMotosData = new MarcasMotosData();
            MarcasMotosModel marcaObj = marcasMotosData.Obtener(MarcaNombre);

            ModelosMotosModel ModeloMotoEditar = new ModelosMotosModel();
            ModeloMotoEditar.Nombre = Nombre;
            ModeloMotoEditar.MarcaId = marcaObj.Id;
            ModeloMotoEditar.Cilindrada = Cilindrada;
            ModeloMotoEditar.TipoMotor = TipoMotor;
            ModeloMotoEditar.Potencia = Potencia;
            ModeloMotoEditar.Peso = Peso;
            ModeloMotoEditar.Tipo = Tipo;
            ModeloMotoEditar.Anyo = Anyo;
            ModeloMotoEditar.Precio = Precio;
            ModeloMotoEditar.Descripcion = Descripcion;
            ModeloMotoEditar.ImagenBase64 = ImagenBase64;

            if (TipoMotor == null) { ModeloMotoEditar.TipoMotor = ""; }
            if (Tipo == null) { ModeloMotoEditar.Tipo = ""; }
            if (Descripcion == null) { ModeloMotoEditar.Descripcion = ""; }
            if (ImagenBase64 == null) { ModeloMotoEditar.ImagenBase64 = ""; }

            ModelosMotosData ModelosMotosData = new ModelosMotosData();
            if (ModelosMotosData.Existe(Nombre))
            {
                bool editado = ModelosMotosData.Editar(ModeloMotoEditar);
                if (!editado)
                {
                    errorMnsj = "Error al editar el Modelo";
                }
                else
                {
                    proceso = "Modelo editado correctamente";
                }
            }
            else
            {
                errorMnsj = "Error, el Modelo no existe";
            }
            return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });

        }
        [HttpGet]
        public IActionResult eliminar(String nombre)
        {
            var proceso = "";
            var errorMnsj = "";

            ModelosMotosData ModelosMotosData = new ModelosMotosData();
            if (ModelosMotosData.Existe(nombre))
            {
                bool eliminado = ModelosMotosData.Eliminar(nombre);
                if (!eliminado)
                {
                    errorMnsj = "Error al eliminar el Modelo";
                }
                else
                {
                    proceso = "Modelo eliminado correctamente";
                }
            }
            else
            {
                errorMnsj = "Error, el Modelo no existe";
            }
            return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });

        }
        public class Mod() { 
            public string Nombre{ set; get; }
        }
    }
}
