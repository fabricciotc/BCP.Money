using System.Text;
using Persistencia;
using MediatR;
using FluentValidation.AspNetCore;
using API.Middleware;
using Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using Aplicacion.Contratos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Aplicacion.Monedas;

using Seguridad.Token_Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.SpaServices.AngularCli;

var builder = WebApplication.CreateBuilder(args);
    
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services = builder.Services;

IConfiguration Configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
services.AddCors(o => o.AddPolicy("corsApp", builder => {
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
services.AddDbContext<MonedasOnlineDbContext>(opt => {
    opt.UseInMemoryDatabase("Monedas");
});
services.AddOptions();
services.AddMediatR(typeof(Consulta.Manejador).Assembly);

services.AddControllers(opt => {
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
}).AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

var builderServices = services.AddIdentityCore<Usuario>();

var identityBuilder = new IdentityBuilder(builderServices.UserType, builder.Services);

identityBuilder.AddRoles<IdentityRole>();
identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();
services.AddControllersWithViews()
        .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

identityBuilder.AddEntityFrameworkStores<MonedasOnlineDbContext>();
identityBuilder.AddSignInManager<SignInManager<Usuario>>();

services.TryAddSingleton<ISystemClock, SystemClock>();
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("SecretKey").Value));
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

services.AddScoped<IUsuarioSesion, UsuarioSesion>();
services.AddScoped<IJwtGenerador, JwtGenerador>();
//DOCUMENTACION
services.AddSwaggerGen(d =>
{
    d.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Servicios para mantenimiento de Monedas y Tipos de Cambio",
        Version = "v1"
    });
    d.CustomSchemaIds(c => c.FullName);
});
services.AddMvc(option => option.EnableEndpointRouting = false);

// In production, the Angular files will be served from this directory
services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "../Presentacion/dist";
});


var app = builder.Build();
app.UseCors("corsApp");
app.UseMiddleware<ManejadorErrorMiddleware>();


//DE MOMENTO LO COMENTO PARA NO USAR HTTPS EN DESARROLLO
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Monedas Online v1");
});
app.UseStaticFiles();
//app.UseSpaStaticFiles();
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller}/{action=Index}/{id?}");
});

app.UseSpa(spa =>
{
    // To learn more about options for serving an Angular SPA from ASP.NET Core,
    // see https://go.microsoft.com/fwlink/?linkid=864501

    spa.Options.SourcePath = "../Presentacion";

    //if (!env.IsDevelopment())
    //{
    //spa.UseAngularCliServer(npmScript: "start");
    //}
});

app.Run();