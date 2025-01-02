using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using System.Text.Json.Serialization;
using Application.GenerateCode.Templates.Core.Abstract;
using Application.GenerateCode.Templates.Core.Entities;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";

        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                var returnUrl = context.Request.Path;
                context.Response.Redirect($"/login?ReturnUrl={returnUrl}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddInfrastructure(configuration)
    .AddApplication();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Host.UseSerilog();
builder.Services.AddSerilog(builder.Configuration);


builder.Services.AddTransient<TemplateEntities>();
builder.Services.AddTransient<TemplateAbstract>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Authentication Scheme: {context.Request.Path}");
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication("Cookies")
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/login";
//        options.LogoutPath = "/logout";
//    });

//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
