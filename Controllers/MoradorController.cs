using Microsoft.AspNetCore.Mvc;
using AttCadastro.Filters;

namespace AttCadastro.Controllers
{
    [AutorizacaoPorCargo("Morador")]
    public class MoradorController : Controller
    {
        public IActionResult Dashboard()
        {
            ViewBag.Nome = HttpContext.Session.GetString("NomeUsuario");
            return View();
        }
    }
}
