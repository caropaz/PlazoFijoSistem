using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PlazoFijoSistem.Datos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// agregar la base de datos
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BaseDeDatos>(options =>
    options.UseSqlServer(@"filename=C:\Databases\PlazoFijoSistem.db")); //Aca especifican la ruta local donde crearon la BD
//Cookie: es lo que el servidor manda al explorador, para tenerlo del lado del explorador
// hacer la configuracion del autorize con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(ConfigurationCookie);  

//
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

app.UseAuthentication();
app.UseAuthorization();

// aca esta el metodo static que necesitan las cookie, esto sirve para devolver las opciones de los siguientes casos
static void ConfigurationCookie(CookieAuthenticationOptions option)
{
    option.LoginPath = "/Login/Index";
    option.AccessDeniedPath = "/Login/NoAutorizado"; // cuando hay acceso denegado
    option.LogoutPath = "/Login/Logout";
    option.ExpireTimeSpan = System.TimeSpan.FromMinutes(5);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
