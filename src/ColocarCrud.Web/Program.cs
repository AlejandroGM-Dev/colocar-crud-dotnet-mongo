using ColocarCrud.Application.Users;
using ColocarCrud.Infrastructure.Data;
using ColocarCrud.Infrastructure.Repositories;
using ColocarCrud.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// Options Pattern para Mongo (desde appsettings.json)
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("Mongo"));

// Inyección de dependencias - Infrastructure
builder.Services.AddSingleton<MongoContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Inyección de dependencias - Application
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Necesario para servir CSS/JS de wwwroot
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ruta por defecto (arranca en Users/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Index}/{id?}");

app.Run();