using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;
services.AddControllersWithViews();

services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
         options =>
         {
             options.LoginPath = new PathString("/Security/Login");
             options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
         });

services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(60);
});

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddOptions();
services.Configure<SqlConfig>(configuration.GetSection(nameof(SqlConfig)));

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ServiceRegister(builder.Configuration));
    containerBuilder.RegisterModule(new Business.ServiceRegister(builder.Configuration));
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

ConfigureServices();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


void ConfigureServices()
{
    //services.AddSingleton<HeaderDisplayName, HeaderDisplayName>();
}
