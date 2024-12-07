using CleanArchProject.Core;
using CleanArchProject.Core.MiddleWares;
using CleanArchProject.Infrastracture;
using CleanArchProject.Infrastracture.Data;
using CleanArchProject.Infrastracture.Interfaces;
using CleanArchProject.Infrastracture.Repositories;
using CleanArchProject.Service;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen().AddCoreDependencies();

//Connection SQL
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
});

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
    .AddServiceDependencies().AddCoreDependencies();

#endregion

#region CORS
var _cors = "_cors";
builder.Services.AddCors(options => options.AddPolicy(name: _cors, policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
}));
#endregion
var app = builder.Build();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
