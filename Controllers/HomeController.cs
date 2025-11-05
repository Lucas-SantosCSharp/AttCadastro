using Microsoft.AspNetCore.Mvc;

namespace AttCadastro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Conta");
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
