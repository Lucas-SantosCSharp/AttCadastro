// ⭐️ PRIMEIRO: Imports necessários
using AttCadastro.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------
// 🔧 CONFIGURAÇÃO DE SERVIÇOS
// -------------------------------------------------------------

// ✅ Configuração do banco de dados (ajuste o nome da conexão conforme appsettings.Development.json)
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"))
);

// ✅ Adiciona suporte a Controllers e Views (MVC)
builder.Services.AddControllersWithViews();

// ✅ Adiciona Autorização (necessário para filtros personalizados)
builder.Services.AddAuthorization();

// ✅ Configuração da sessão (armazenamento temporário de login)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // tempo máximo da sessão inativa
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// -------------------------------------------------------------
// 🚀 CONFIGURAÇÃO DO PIPELINE HTTP
// -------------------------------------------------------------

// ✅ Tratamento de exceções
if (!app.Environment.IsDevelopment())
{
    // Modo Produção
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Modo Desenvolvimento — mantém erros detalhados
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Sessão e Autorização devem vir antes de MapControllerRoute
app.UseSession();
app.UseAuthorization();

// ✅ Rota padrão do sistema
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Conta}/{action=Login}/{id?}"
);

app.Run();
