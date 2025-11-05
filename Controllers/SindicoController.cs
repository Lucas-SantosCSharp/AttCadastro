using Microsoft.AspNetCore.Mvc;
using AttCadastro.Filters;

namespace AttCadastro.Controllers
{
    [AutorizacaoPorCargo("Síndico")]
    public class SindicoController : Controller
    {
        public IActionResult Dashboard()
        {
            ViewBag.Nome = HttpContext.Session.GetString("NomeUsuario");
            return View();
        }
    }
}
