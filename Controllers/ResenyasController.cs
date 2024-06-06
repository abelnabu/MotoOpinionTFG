using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using MotoOpinion.Data;
using MotoOpinion.Models;
using System.Diagnostics;
using System.Text.Json;
using System.IO;
using System.Security.Cryptography;
using System.Buffers.Text;
using static MotoOpinion.Controllers.ModelosMotosController;
using SendEmail.Services.SendEmail.Services;

namespace MotoOpinion.Controllers
{
    public class ResenyasController : Controller
    {
        public IActionResult Index(String error,String procesoRealizado, int paginacion)
        {
            ViewData["Title"] = "Modelos Motos";
            ResenyasData ResenyasData = new ResenyasData();
            List<ResenyasModel> listaResenyas = ResenyasData.Listar();
            
            var paginas = listaResenyas.Count() / 10;
            var restoPaginas = listaResenyas.Count() % 10;

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

            List<ResenyasModel> listaResenyasPaginada = new List<ResenyasModel>();
            for (int i = registroInicial; i <= registroInicial+10; i++)
            {
                if (listaResenyas.Count() > i)
                {
                    listaResenyasPaginada.Add(listaResenyas[i]);
                }
            }

            ViewBag.Resenyas = listaResenyasPaginada;

            if (error != null && error != "") {
                ViewData["Error"] = error;
            }
            if (procesoRealizado != null && procesoRealizado != "")
            {
                ViewData["ProcesoRealizado"] = procesoRealizado;
            }

            var nombreUsu = HttpContext.Session.GetString("NombreUsuario");

            if (nombreUsu != null)
            {
                UsuariosData usuario = new UsuariosData();
                UsuariosModel usuarioMod = usuario.Obtener(nombreUsu.ToString());
                ViewData["RolUsuario"] = usuarioMod.Rol;
                ViewData["UsuarioNombre"] = usuarioMod.Nombre;
            }
            else
            {
                ViewData["RolUsuario"] = "";
                ViewData["UsuarioNombre"] = "";
            }

            return View();
        }


        [HttpGet]
        public IActionResult leerResenya(String idResenya, String error, String procesoRealizado)
        {
            var nombreUsu = HttpContext.Session.GetString("NombreUsuario");
            

            UsuariosData usuario = new UsuariosData();
            UsuariosModel usuarioMod = usuario.Obtener(nombreUsu.ToString());

            if (error != null && error != "")
            {
                ViewData["Error"] = error;
            }
            if (procesoRealizado != null && procesoRealizado != "")
            {
                ViewData["ProcesoRealizado"] = procesoRealizado;
            }

            ResenyasData ResenyasData = new ResenyasData();
            var ModeloResenya = ResenyasData.Obtener(Convert.ToInt32(idResenya));
            ViewData["UsuarioNombre"] = ModeloResenya.UsuarioNombre;

            ComentariosData ComentariosData = new ComentariosData();
            var comentarios = ComentariosData.ListarComentariosResenya(Convert.ToInt32(idResenya));
            ViewBag.Comentarios = comentarios;

            SuscripcionesData suscripciones = new SuscripcionesData();
            SuscripcionesModel SusMod = new SuscripcionesModel();
            SusMod.UsuarioMaestro = ModeloResenya.Usuario;
            SusMod.UsuarioSuscriptor = usuarioMod.Id;

            IList<SuscripcionesModel> subs = suscripciones.EstaSuscrito(SusMod);
            if (subs.Count() > 0)
            {
                ViewData["estaSuscrito"] = "true";
            }
            else {
                ViewData["estaSuscrito"] = "false";
            }

            return View("Resenya",ModeloResenya);
        }

        [HttpGet]
        public IActionResult crear()
        {
            ModelosMotosData Modelos = new ModelosMotosData();
            IList<ModelosMotosModel> ModelosMotosModel = Modelos.Listar();

            ViewBag.Modelos = ModelosMotosModel;
            return View("Crear");
        }

        [HttpPost]
        public IActionResult crear(string Titulo, DateTime Fecha,int Modelo,string ImagenBase64,string Texto)
        {
            var errorMnsj = "";
            var proceso = "";
            var nombreUsu = HttpContext.Session.GetString("NombreUsuario");

            ResenyasData ResenyasData = new ResenyasData();
            UsuariosData usuario = new UsuariosData();
            UsuariosModel usuarioMod = usuario.Obtener(nombreUsu.ToString());

            ResenyasModel ResenyaCrear = new ResenyasModel();
            ResenyaCrear.Titulo = Titulo;
            ResenyaCrear.Fecha = Fecha;
            ResenyaCrear.ModeloMoto = Modelo;
            ResenyaCrear.ImagenBase64 = ImagenBase64;
            ResenyaCrear.Texto = Texto;
            ResenyaCrear.Usuario = usuarioMod.Id;
            
            if (ImagenBase64 == null) { ResenyaCrear.ImagenBase64 = ""; }

            bool creardo = ResenyasData.Crear(ResenyaCrear);
            if (!creardo)
            {
                errorMnsj = "Error al crear la reseña";
            }
            else
            {
                proceso = "Reseña creada correctamente";

                SuscripcionesData Sus = new SuscripcionesData();

                List<SuscripcionesModel> suscrpciones = Sus.Listar(usuarioMod.Id);
                foreach (var susc in suscrpciones)
                {
                    UsuariosData usuariosData = new UsuariosData();
                    UsuariosModel usu = usuariosData.ObtenerPorId(susc.UsuarioSuscriptor);

                    EmailService emailS = new();
                    EmailDTO emailDTO = new EmailDTO();
                    emailDTO.Para = usu.Email;
                    emailDTO.Asunto = nombreUsu + " ha creado una nueva review en MotoOpinion";
                    emailDTO.Contenido = "No te pierdas la nueva review de " + nombreUsu + ". Ve a leerla a MotoOpinon!";
                    emailS.SendEmail(emailDTO);
                }
            }
            
            return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });

        }

        [HttpPost]
        public IActionResult crearComentario(int Rating, string Texto, int IdResenya)
        {
            var errorMnsj = "";
            var proceso = "";

            var nombreUsu = HttpContext.Session.GetString("NombreUsuario");
            UsuariosData usuario = new UsuariosData();
            UsuariosModel usuarioMod = usuario.Obtener(nombreUsu.ToString());

            ComentariosData comentarios = new ComentariosData();

            ComentariosModel comentariosModel = new ComentariosModel();
            comentariosModel.Fecha = System.DateTime.Now;
            comentariosModel.IdResenya = IdResenya;
            comentariosModel.Usuario = usuarioMod.Id;
            comentariosModel.Texto = Texto;
            comentariosModel.Puntuacion = Rating;

            bool ComentarioCreado = comentarios.Crear(comentariosModel);

            if (!ComentarioCreado)
            {
                errorMnsj = "Error al crear el comentario";
            }
            else
            {
                proceso = "Comentario publicado";
            }

            return RedirectToAction("leerResenya", new { error = errorMnsj, procesoRealizado = proceso, idResenya = IdResenya });

        }

        [HttpPost]
        public JsonResult suscribirse(Mod mod)
        {
            UsuariosData UsuData = new UsuariosData();
            var ModeloUsu = UsuData.Obtener(mod.Nombre);

            var nombreUsu = HttpContext.Session.GetString("NombreUsuario");
            UsuariosData usuario = new UsuariosData();
            UsuariosModel usuarioMod = usuario.Obtener(nombreUsu.ToString());

            SuscripcionesData Sus = new SuscripcionesData();
            SuscripcionesModel SusMod = new SuscripcionesModel();
            SusMod.UsuarioMaestro = ModeloUsu.Id;
            SusMod.UsuarioSuscriptor = usuarioMod.Id;

            bool creado = false;

            if (SusMod.UsuarioMaestro == SusMod.UsuarioSuscriptor)
            {
                creado = false;
            }else {
                creado = Sus.Crear(SusMod);
            }
            
            string jsonString = JsonSerializer.Serialize(creado);
            return Json(jsonString);
        }

        [HttpPost]
        public JsonResult desSuscribirse(Mod mod)
        {
            UsuariosData UsuData = new UsuariosData();
            var ModeloUsu = UsuData.Obtener(mod.Nombre);

            var nombreUsu = HttpContext.Session.GetString("NombreUsuario");
            UsuariosData usuario = new UsuariosData();
            UsuariosModel usuarioMod = usuario.Obtener(nombreUsu.ToString());

            SuscripcionesData Sus = new SuscripcionesData();
            SuscripcionesModel SusMod = new SuscripcionesModel();
            SusMod.UsuarioMaestro = ModeloUsu.Id;
            SusMod.UsuarioSuscriptor = usuarioMod.Id;

            bool cancelado = false;

            if (SusMod.UsuarioMaestro == SusMod.UsuarioSuscriptor)
            {
                cancelado = false;
            }
            else
            {
                cancelado = Sus.Eliminar(SusMod);
            }

            string jsonString = JsonSerializer.Serialize(cancelado);
            return Json(jsonString);
        }

        //[HttpPost]
        //public IActionResult editar(string Nombre, string MarcaNombre, int Cilindrada, string TipoMotor, decimal Potencia, decimal Peso, string Tipo, int Anyo, decimal Precio, string Descripcion, string ImagenBase64)
        //{
        //    var proceso = "";
        //    var errorMnsj = "";

        //    ResenyasData ResenyasData = new ResenyasData();
        //    MarcasMotosModel marcaObj = ResenyasData.Obtener(MarcaNombre);

        //    ResenyasModel ModeloMotoEditar = new ResenyasModel();
        //    ModeloMotoEditar.Nombre = Nombre;
        //    ModeloMotoEditar.MarcaId = marcaObj.Id;
        //    ModeloMotoEditar.Cilindrada = Cilindrada;
        //    ModeloMotoEditar.TipoMotor = TipoMotor;
        //    ModeloMotoEditar.Potencia = Potencia;
        //    ModeloMotoEditar.Peso = Peso;
        //    ModeloMotoEditar.Tipo = Tipo;
        //    ModeloMotoEditar.Anyo = Anyo;
        //    ModeloMotoEditar.Precio = Precio;
        //    ModeloMotoEditar.Descripcion = Descripcion;
        //    ModeloMotoEditar.ImagenBase64 = ImagenBase64;

        //    if (TipoMotor == null) { ModeloMotoEditar.TipoMotor = ""; }
        //    if (Tipo == null) { ModeloMotoEditar.Tipo = ""; }
        //    if (Descripcion == null) { ModeloMotoEditar.Descripcion = ""; }
        //    if (ImagenBase64 == null) { ModeloMotoEditar.ImagenBase64 = ""; }

        //    ResenyasData ResenyasData = new ResenyasData();
        //    if (ResenyasData.Existe(Nombre))
        //    {
        //        bool editado = ResenyasData.Editar(ModeloMotoEditar);
        //        if (!editado)
        //        {
        //            errorMnsj = "Error al editar el Modelo";
        //        }
        //        else
        //        {
        //            proceso = "Modelo editado correctamente";
        //        }
        //    }
        //    else
        //    {
        //        errorMnsj = "Error, el Modelo no existe";
        //    }
        //    return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });

        //}
        //[HttpGet]
        //public IActionResult eliminar(String nombre)
        //{
        //    var proceso = "";
        //    var errorMnsj = "";

        //    ResenyasData ResenyasData = new ResenyasData();
        //    if (ResenyasData.Existe(nombre))
        //    {
        //        bool eliminado = ResenyasData.Eliminar(nombre);
        //        if (!eliminado)
        //        {
        //            errorMnsj = "Error al eliminar el Modelo";
        //        }
        //        else
        //        {
        //            proceso = "Modelo eliminado correctamente";
        //        }
        //    }
        //    else
        //    {
        //        errorMnsj = "Error, el Modelo no existe";
        //    }
        //    return RedirectToAction("Index", new { error = errorMnsj, procesoRealizado = proceso });

        //}
        public class Res()
        {
            public string idResenya { set; get; }
        }
    }
}
