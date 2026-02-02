using Microsoft.AspNetCore.Mvc;
using Dominio;

namespace AplicacionWeb.Controllers
{
    public class VuelosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Listado(string? iata)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Cliente") return View("NoAutorizado");
            if (string.IsNullOrEmpty(iata))
            {
                ViewBag.Listado = miSistema.Vuelos;
            }
            else
            {
                ViewBag.Listado = miSistema.BuscarVuelosPorAeropuerto(iata);
            }
            return View();
        }
    }
}
