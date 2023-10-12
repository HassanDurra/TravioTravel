using Microsoft.EntityFrameworkCore;
using TravioHotel.DataContext;
using TravioHotel.Services;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Custom Services Dependency Injections
builder.Services.AddScoped<MailServer, MailServer>();
builder.Services.AddScoped<RandomGenerate , RandomGenerate>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
// Connection String Calling
builder.Services.AddDbContext<DatabaseContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
