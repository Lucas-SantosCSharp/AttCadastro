using Microsoft.AspNetCore.Mvc;
using AttCadastro.Filters;

namespace AttCadastro.Controllers
{
    [AutorizacaoPorCargo("Administrador")]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            ViewBag.Nome = HttpContext.Session.GetString("NomeUsuario");
            return View();
        }
    }
}
