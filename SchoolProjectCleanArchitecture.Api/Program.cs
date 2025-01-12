using CleanArchProject.Core;
using CleanArchProject.Core.Filters;
using CleanArchProject.Core.MiddleWares;
using CleanArchProject.Data.Entities.Identies;
using CleanArchProject.Data.Entities.Identities;
using CleanArchProject.Infrastracture;
using CleanArchProject.Infrastracture.Data;
using CleanArchProject.Infrastracture.DataSeeder;
using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Infrastracture.Repositories;
using CleanArchProject.Service;
using dotenv.net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen().AddCoreDependencies();

////Connection SQL
//builder.Services.AddDbContext<AppDbContext>(option =>
//{
//    option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
//});

#region Localization
    builder.Services.AddControllersWithViews();
    builder.Services.AddLocalization(opt =>
    {
        opt.ResourcesPath = "";
    });

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        List<CultureInfo> supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("ar-EG"),
            new CultureInfo("en-US"),
            new CultureInfo("de-DE"),
            new CultureInfo("fr-FR"),
            new CultureInfo("en-GB")
        };

        options.DefaultRequestCulture = new RequestCulture("ar-EG");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

#endregion

#region Dependency injection

builder.Services.AddInfrastractureDependencies()
    .AddServiceDependencies().AddCoreDependencies().AddServiceRegistration(builder.Configuration);

#endregion

#region CORS
var _cors = "AllowAll";
builder.Services.AddCors(options => options.AddPolicy(name: _cors, policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
}));
#endregion

builder.Services.AddTransient<AuthFilter>();
var app = builder.Build();

#region DataSeeding
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var rolManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await RoleSeeder.SeedAsync(rolManager);
    await UserSeeder.SeedAsync(userManager);
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region Localization middleware

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

#endregion

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors(_cors);

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
