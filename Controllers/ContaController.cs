using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AttCadastro.Context;
using System.Linq;
using AttCadastro.Models;
using AttCadastro.Utils;

namespace AttCadastro.Controllers
{
    public class ContaController : Controller
    {
        private readonly AgendaContext _context;

        public ContaController(AgendaContext context)
        {
            _context = context;
        }

        // ==================== LOGIN (GET) ====================
        public IActionResult Login()
        {
            return View();
        }

        // ==================== LOGIN (POST) ====================
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            string senhaHash = Criptografia.GerarHashSha256(senha);

            var pessoa = _context.Pessoas
                .FirstOrDefault(p => p.Email == email && p.Senha == senhaHash);

            if (pessoa != null)
            {
                // Cria sessão
                HttpContext.Session.SetString("CargoUsuario", pessoa.Cargo);
                HttpContext.Session.SetString("NomeUsuario", pessoa.Nome);
                HttpContext.Session.SetString("EmailUsuario", pessoa.Email);

                // Redireciona conforme cargo
                if (pessoa.Cargo == "Administrador")
                    return RedirectToAction("Dashboard", "Admin");

                else if (pessoa.Cargo == "Síndico" || pessoa.Cargo == "Sindico")
                    return RedirectToAction("Dashboard", "Sindico");

                else
                    return RedirectToAction("Dashboard", "Morador");
            }

            ViewBag.Erro = "Credenciais inválidas. Verifique seu email e senha.";
            return View();
        }

        // ==================== CADASTRO (GET) ====================
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        // ==================== CADASTRO (POST) ====================
        [HttpPost]
        public IActionResult Registrar(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                // Define cargo padrão (se não informado)
                if (string.IsNullOrEmpty(pessoa.Cargo))
                    pessoa.Cargo = "Morador";

                // Criptografa a senha
                pessoa.Senha = Criptografia.GerarHashSha256(pessoa.Senha);

                // Salva no banco
                _context.Pessoas.Add(pessoa);
                _context.SaveChanges();

                // Após cadastrar, redireciona para o login
                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso! Faça login para continuar.";
                return RedirectToAction("Login", "Conta");
            }

            return View(pessoa);
        }

        // ==================== LOGOUT ====================
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Conta");
        }
    }
}
