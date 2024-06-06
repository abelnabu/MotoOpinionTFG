using Microsoft.AspNetCore.Mvc;
using MotoOpinion.Data;
using MotoOpinion.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MotoOpinion.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index(String error,String procesoRealizado)
        {
            ViewData["Title"] = "Usuarios";
            UsuariosData usuariosData = new UsuariosData();
            List<UsuariosModel> listaUsuarios = usuariosData.Listar();
            ViewBag.Usuarios = listaUsuarios;

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
        public JsonResult leerUsuario(Usu usuario )
        {
            UsuariosData usuariosData = new UsuariosData();
            var Usuario = usuariosData.Obtener(usuario.Nombre);
            string jsonString = JsonSerializer.Serialize(Usuario);
            return Json(jsonString);
        }

        [HttpPost]
        public IActionResult crear(String nombre, String email, String clave, String rol) 
        {
            var errorMnsj = "";
            var proceso = "";
            UsuariosModel usuarioCrear = new UsuariosModel();
            usuarioCrear.Nombre = nombre;
            usuarioCrear.Email = email;
            usuarioCrear.Clave = clave;
            usuarioCrear.Rol = rol;

            UsuariosData usuariosData = new UsuariosData();
            if (!usuariosData.Existe(nombre))
            {
                bool creardo = usuariosData.Crear(usuarioCrear);
                if (!creardo)
                {
                    errorMnsj = "Error al crear el usuario";
                }
                else {
                    proceso = "Usuario creado correctamente";
                }
            }
            else
            {
                errorMnsj = "Error, el usuario ya existe";
            }
            return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });
           
        }

        [HttpPost]
        public IActionResult editar(String nombre, String email, String clave, String rol)
        {
            var proceso = "";
            var errorMnsj = "";

            UsuariosModel usuarioEditar = new UsuariosModel();
            usuarioEditar.Nombre = nombre;
            usuarioEditar.Email = email;
            usuarioEditar.Clave = clave;
            usuarioEditar.Rol = rol;

            UsuariosData usuariosData = new UsuariosData();
            if (usuariosData.Existe(nombre))
            {
                bool editado = usuariosData.Editar(usuarioEditar);
                if (!editado)
                {
                    errorMnsj = "Error al editar el usuario";
                }
                else
                {
                    proceso = "Usuario editado correctamente";
                }
            }
            else
            {
                errorMnsj = "Error, el usuario no existe";
            }
            return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });

        }
        [HttpGet]
        public IActionResult eliminar(String nombre)
        {
            var proceso = "";
            var errorMnsj = "";

            UsuariosData usuariosData = new UsuariosData();
            if (usuariosData.Existe(nombre))
            {
                bool eliminado = usuariosData.Eliminar(nombre);
                if (!eliminado)
                {
                    errorMnsj = "Error al eliminar el usuario";
                }
                else
                {
                    proceso = "Usuario eliminado correctamente";
                }
            }
            else
            {
                errorMnsj = "Error, el usuario no existe";
            }
            return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });

        }
        public class Usu() { 
            public string? Nombre { set; get; }
        }
    }
}
