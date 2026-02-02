using Microsoft.AspNetCore.Mvc;
using Dominio;

namespace AplicacionWeb.Controllers
{
    public class UsuariosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult AltaOcasional()
        {
            if (HttpContext.Session.GetString("rol") != null) return View("NoAutorizado");
            return View();
        }

        [HttpPost]
        public IActionResult AltaOcasional(string correo, string pass, string cedula, string nombre, string nacionalidad)
        {
            try
            {
                if (string.IsNullOrEmpty(correo)) throw new Exception("El correo no puede ser vacío.");
                if (string.IsNullOrEmpty(pass)) throw new Exception("La contraseña no puede ser vacía.");
                if (pass.Length < 8) throw new Exception("La contraseña debe tener al menos 8 dígitos");
                if (string.IsNullOrEmpty(cedula)) throw new Exception("La cédula no puede ser vacía.");
                if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre no puede ser vacío.");
                if (string.IsNullOrEmpty(nacionalidad)) throw new Exception("La nacionalidad no puede ser vacía.");

                bool regalo = new Random().Next(0, 2) == 1;

                Ocasional o = new Ocasional(correo, pass, cedula, nombre, nacionalidad, regalo);
                miSistema.AgregarUsuario(o);

                HttpContext.Session.SetString("correo", o.Correo);
                HttpContext.Session.SetString("rol", "Cliente");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Correo = correo;
                ViewBag.Cedula = cedula;
                ViewBag.Nombre = nombre;
                ViewBag.Nacionalidad = nacionalidad;
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Listado()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin") return View("NoAutorizado");
            ViewBag.Listado = miSistema.ListarClientesOrdenadosPorCedula();
            return View();
        }

        [HttpGet]
        public IActionResult Modificar(string? cedula)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin") return View("NoAutorizado");
            if (string.IsNullOrEmpty(cedula)) return RedirectToAction("Listado");
            else
            {
                Usuario u = miSistema.BuscarClientePorCedula(cedula);
                if (u == null) throw new Exception("No se encontró cliente.");
                else
                {
                    ViewBag.Usuario = u;
                }
                return View();
            }
        }

        [HttpPost]
        public IActionResult Modificar(string cedula, bool nuevoRegalo, int nuevoPuntos)
        {
            try
            {
                if (string.IsNullOrEmpty(cedula)) throw new Exception("La cédula no puede estar vacía.");
                Usuario u = miSistema.BuscarClientePorCedula(cedula);
                if (u == null) throw new Exception("Usuario no encontrado.");

                if (u is Premium p)
                {
                    if (nuevoPuntos < 0) throw new Exception("Los puntos no pueden ser negativos.");
                    miSistema.ModificarPuntosUsuario(cedula, nuevoPuntos);
                    TempData["Exito"] = $"Puntos del Cliente {p.Nombre} ({p.Cedula}) modificado";
                }
                if (u is Ocasional o)
                {
                    miSistema.ModificarRegaloUsuario(cedula, nuevoRegalo);
                    TempData["Exito"] = $"Regalo del Cliente {o.Nombre} ({o.Cedula}) modificado";
                }
                return RedirectToAction("Listado");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuario = miSistema.BuscarClientePorCedula(cedula);
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("rol") != null) return View("NoAutorizado");
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo, string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(correo)) throw new Exception("El correo no puede ser vacío.");
                if (string.IsNullOrEmpty(pass)) throw new Exception("La contraseña no puede ser vacía.");
                Usuario u = miSistema.Login(correo, pass);
                if (u == null) throw new Exception("Email o contraseña inválidos.");
                HttpContext.Session.SetString("correo", u.Correo);
                HttpContext.Session.SetString("rol", u.Rol());
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.correo = correo;
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("rol") == null) return View("NoAutorizado");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult VerPerfil()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Cliente") return View("NoAutorizado");
            ViewBag.Cliente = miSistema.BuscarUsuarioPorCorreo(HttpContext.Session.GetString("correo"));
            return View();
        }
    }
}