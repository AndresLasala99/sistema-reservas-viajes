using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AplicacionWeb.Controllers
{
    public class PasajesController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Listado()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin") return View("NoAutorizado");
            ViewBag.Listado = miSistema.ListarPasajesOrdenadosPorFecha();
            return View();
        }

        [HttpGet]
        public IActionResult HacerCompra(string codigoVuelo)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Cliente") return View("NoAutorizado");
            ViewBag.Vuelo = miSistema.BuscarVueloPorCodigo(codigoVuelo);
            return View();
        }

        [HttpPost]
        public IActionResult HacerCompra(string codigoVuelo, DateTime fecha, TipoEquipaje equipaje)
        {
            try
            {
                if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Cliente") throw new Exception("Solo clientes pueden comprar pasajes.");
                if (string.IsNullOrEmpty(codigoVuelo)) throw new Exception("El código de vuelo no puede ser nulo");
                if (fecha < DateTime.Today) throw new Exception("La fecha no puede ser anterior a hoy.");

                miSistema.CrearCompra(codigoVuelo, fecha,equipaje, HttpContext.Session.GetString("correo"));
                TempData["Exito"] = $"Usted a comprado exitosamente el vuelo {codigoVuelo}.";
                return RedirectToAction("Listado", "Vuelos");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Vuelo = miSistema.BuscarVueloPorCodigo(codigoVuelo);
                return View();
            }
        }

        [HttpGet]
        public IActionResult ListadoDeUnCliente()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Cliente") return View("NoAutorizado");
            ViewBag.ListadoDeUnCliente = miSistema.PasajesDeUnCliente(HttpContext.Session.GetString("correo"));
            return View();
        }
    }
}