using SocialMedia.Core.Application;
using SocialMedia.Infraestructure.Identity;
using SocialMedia.Infraestructure.Persistence;
using SocialMedia.Infraestructure.Shared;
using WebApp.SocialMedia.MiddleWares;
using WebApp.StockApp.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();

builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddIdentityInfraestruture(builder.Configuration);
builder.Services.AddSharedInfraestructure(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
// Add services to the container.
builder.Services.AddControllersWithViews();

//sesiones
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ValidateUserSession>();

var app = builder.Build();

await app.AddIdentitySeeds();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
