using eticaret.Data;
using eticaret.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Builder; // Eklenen
using Microsoft.Extensions.DependencyInjection; // Eklenen
using Microsoft.Extensions.Hosting; // Eklenen
using System; // Eklenen

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// PaymentService'i ekleyin
  // PaymentService'in bulunduðu ad alaný



// Servisleri ekleyin
builder.Services.AddScoped<PaymentService>();

// Diðer ayarlar ve kodlar



// Add DbContext as a service
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add IHttpContextAccessor service
builder.Services.AddHttpContextAccessor();

// Add distributed memory cache and session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturumun süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Add session middleware
app.UseSession();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();
