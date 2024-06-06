using Microsoft.AspNetCore.Mvc;
using MotoOpinion.Data;
using MotoOpinion.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MotoOpinion.Controllers
{
    public class MarcasMotosController : Controller
    {
        public IActionResult Index(String error,String procesoRealizado)
        {
            ViewData["Title"] = "Marcas motos";
            MarcasMotosData MarcasMotosData = new MarcasMotosData();
            List<MarcasMotosModel> listaMarcas = MarcasMotosData.Listar();
            ViewBag.Marcas = listaMarcas;

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
        public JsonResult leermarca(Marca marca )
        {
            MarcasMotosData MarcasMotosData = new MarcasMotosData();
            var marcaObj = MarcasMotosData.Obtener(marca.Nombre);
            string jsonString = JsonSerializer.Serialize(marcaObj);
            return Json(jsonString);
        }

        [HttpPost]
        public IActionResult crear(String nombre) 
        {
            var errorMnsj = "";
            var proceso = "";
            MarcasMotosModel marcaCrear = new MarcasMotosModel();
            marcaCrear.Nombre = nombre;

            MarcasMotosData MarcasMotosData = new MarcasMotosData();
            if (!MarcasMotosData.Existe(nombre))
            {
                bool creardo = MarcasMotosData.Crear(marcaCrear);
                if (!creardo)
                {
                    errorMnsj = "Error al crear la marca";
                }
                else {
                    proceso = "Marca creada correctamente";
                }
            }
            else
            {
                errorMnsj = "Error, la marca ya existe";
            }
            return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });
           
        }

        
        [HttpGet]
        public IActionResult eliminar(String nombre)
        {
            var proceso = "";
            var errorMnsj = "";

            MarcasMotosData MarcasMotosData = new MarcasMotosData();
            if (MarcasMotosData.Existe(nombre))
            {
                bool eliminado = MarcasMotosData.Eliminar(nombre);
                if (!eliminado)
                {
                    errorMnsj = "Error al eliminar la marca";
                }
                else
                {
                    proceso = "Marca eliminada correctamente";
                }
            }
            else
            {
                errorMnsj = "Error, la marca no existe";
            }
            return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });

        }
        public class Marca() { 
            public string? Nombre { set; get; }
        }
    }
}
