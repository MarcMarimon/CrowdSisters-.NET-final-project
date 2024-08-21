using CrowdSisters.Conections;
using CrowdSisters.DAL;
using CrowdSisters.Models;
using CrowdSisters.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the Connection class with dependency injection
builder.Services.AddScoped<Connection>();
builder.Services.AddScoped<ServiceProyecto>();
builder.Services.AddScoped<ServiceCrearProyecto>();
builder.Services.AddScoped<ServiceLogin>();
builder.Services.AddScoped<FirebaseService>();
builder.Services.AddScoped<ServiceCategoria>();
builder.Services.AddScoped<ServiceSubcategoria>();
builder.Services.AddScoped<ServiceRecompensa>();
builder.Services.AddScoped<DALProyecto>();
builder.Services.AddScoped<DALDonacion>();
builder.Services.AddScoped<DALUsuario>();
builder.Services.AddScoped<DALCategoria>();
builder.Services.AddScoped<DALRecompensa>();
builder.Services.AddScoped<DALSubcategoria>();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(18000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
