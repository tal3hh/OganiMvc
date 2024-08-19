using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OganiApp.Core.Entities;
using OganiApp.Data.Contexts;
using OganiApp.Data.UniteOfWork;
using OganiApp.Service.Extensions;
using OganiApp.Service.Utilities.CustomDescriber;

var builder = WebApplication.CreateBuilder(args);

//Extension
builder.Services.AddValidation();
builder.Services.AddServices();


builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

#region Identity

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;  //Simvollardan biri olmalidir(@,/,$) 
    opt.Password.RequireLowercase = true;         //Mutleq Kicik herf
    opt.Password.RequireUppercase = true;         //Mutleq Boyuk herf 
    opt.Password.RequiredLength = 4;              //Min. simvol sayi
    opt.Password.RequireDigit = false;

    opt.User.RequireUniqueEmail = true;

    opt.SignIn.RequireConfirmedEmail = true;
    opt.SignIn.RequireConfirmedAccount = false;

    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); //Sifreni 5 defe sehv girdikde hesab 3dk baglanir.
    opt.Lockout.MaxFailedAccessAttempts = 5;                      //Sifreni max. 5defe sehv girmek olar.

}).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

#endregion

#region Cookie

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "AshionIdentity";
    opt.LoginPath = new PathString("/Account/Login");
    opt.AccessDeniedPath = new PathString("/Account/AccessDenied");

});

#endregion

#region Context

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration["ConnectionStrings:Mssql"]);
});

#endregion

builder.Services.AddScoped<IUow, Uow>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Default2",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
        );
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=Home}/{action=HomePage}/{id?}"
        );
    endpoints.MapDefaultControllerRoute();
});

app.Run();
