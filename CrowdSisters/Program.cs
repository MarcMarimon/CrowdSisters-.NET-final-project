using CrowdSisters.Conections;
using CrowdSisters.DAL;
using CrowdSisters.Repository;
using CrowdSisters.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the Connection class with dependency injection
builder.Services.AddScoped<Connection>();
builder.Services.AddScoped<DALProyecto>();
builder.Services.AddScoped<RepoProyecto>();
builder.Services.AddScoped<ServiceProyecto>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
