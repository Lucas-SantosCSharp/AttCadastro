using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AttCadastro.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AutorizacaoPorCargoAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _cargoPermitido;

        public AutorizacaoPorCargoAttribute(string cargoPermitido)
        {
            _cargoPermitido = cargoPermitido;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var cargo = context.HttpContext.Session.GetString("CargoUsuario");

            if (string.IsNullOrEmpty(cargo))
            {
                context.Result = new RedirectToActionResult("Login", "Conta", null);
                return;
            }

            if (!cargo.Equals(_cargoPermitido, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new RedirectToActionResult("AcessoNegado", "Home", null);
            }
        }
    }
}
