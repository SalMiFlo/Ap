using AplicacionEFCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//-------------------------------------------------------------Solo accertar usuarios autenticados
var politicaUsuariosUtenticados = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

// Add services to the container.
builder.Services.AddControllersWithViews(opciones => 
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosUtenticados));
});
//-------------------------------------------------------------Solo accertar usuarios autenticados
//---------------------------------------------------------------------------------------------------------------------
builder.Services.AddDbContext<ApplicationDbSetContext>(opciones => opciones.UseSqlServer("name=DefaultConecction")); //Servicio para la conexion a la base de datos
builder.Services.AddAuthentication(); //Para utilizar las herramientas de autentificacion de Identity
//Esto ultimo indica que no necesitas una cuenta confirmada
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDbSetContext>().AddDefaultTokenProviders(); //Esto ultimo indica que no necesitas una cuenta confirmada
//Configuración para diseñar nuestras propias vistas
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, opciones =>
{
    opciones.LoginPath = "/usuarios/Login";
    opciones.AccessDeniedPath = "/usuarios/Login";
});
//-----------------------------------------------------------------------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Obtener la información del usuario autenticado
app.UseAuthentication();
//
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Index}/{id?}");

app.Run();
