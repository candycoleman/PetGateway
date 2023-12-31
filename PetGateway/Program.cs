using Microsoft.EntityFrameworkCore;
using PetGateway.Models;
using PetGateway.Repository;

var builder = WebApplication.CreateBuilder(args);

//Add DI
builder.Services.AddTransient<IPetGatewayRepository, PetGatewayRepository>();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "pet",
    pattern: "{controller=Pet}/{action=Index}/{ownerId?}");



app.Run();
