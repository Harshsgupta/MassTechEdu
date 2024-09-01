using MassTechEdu.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddDbContext<MasstechEduContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout (optional)
    options.Cookie.HttpOnly = true; // Ensure the session cookie is HTTP only
    options.Cookie.IsEssential = true; // Mark the session cookie as essential
});

//Step 1
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    option.LoginPath = "/Auth/Login";
    option.AccessDeniedPath = "/Auth/Login";
    option.LogoutPath= "/Auth/Login";
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Step 2
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
