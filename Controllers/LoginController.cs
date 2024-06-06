using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotoOpinion.Data;
using MotoOpinion.Models;

namespace MotoOpinion.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            ViewData["Title"] = "Login";
            return View("Login");
        }

        [HttpPost]
        public ActionResult CompruebaPass(String username, String password) {
            UsuariosData data = new UsuariosData();

            var passCifrada = Recursos.cifrarPasswordSHA256(password);
            bool existe = data.ComprobarUsuario(username, passCifrada);

            if (existe) {
                UsuariosModel usuario = data.Obtener(username);

                if (usuario.Nombre != null && usuario.Nombre != "")
                {
                    HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);
                }
                return RedirectToAction("Index","Resenyas");
            }
            else {

                ViewData["error"] = "Usuario o contraseña erróneo";
                return View("Login");
            }

        }

        [HttpPost]
        public String ObtenerPermisos()
        {
            var nombreUsu = HttpContext.Session.GetString("NombreUsuario");

            if (nombreUsu != null)
            {
                UsuariosData usuario = new UsuariosData();
                UsuariosModel usuarioMod = usuario.Obtener(nombreUsu.ToString());
                return usuarioMod.Rol;
            }
            else
            {
                return "";
            }

        }
    }
}
