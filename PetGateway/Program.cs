using Microsoft.EntityFrameworkCore;
using PetGateway.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add EF Core DI
builder.Services.AddDbContext<GatewayContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GatewayContext")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "pet",
    pattern: "pets/{action=Index}/{id?}",
    defaults: new { controller = "Pet" });

app.MapControllerRoute(
    name: "owner",
    pattern: "owners/{action=Index}/{id?}",
    defaults: new { controller = "Owner" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
