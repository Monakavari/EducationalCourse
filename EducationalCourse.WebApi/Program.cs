using EducationalCourse.Framework.Infrastructure.Extensions;
using EducationalCourse.IOC;
using Microsoft.OpenApi.Models;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
//var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.SchemaGeneratorOptions.IgnoreObsoleteProperties = true;
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sample Project",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
        }
    });
});

#endregion Swagger

#region IOC

DependencyContainer.ConfigureServices(builder.Configuration, builder.Services);

#endregion IOC

var app = builder.Build();

app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseJWTHandler();

app.Run();


#region Logger
//Run(logger);

//void Run(NLog.Logger logger)
//{
//    try
//    {
//        logger.Debug("Initialize Main");
//        app.Run();
//    }
//    catch (Exception ex)
//    {
//        logger.Error(ex, "Stopped program because of exception");
//        throw;
//    }
//    finally
//    {
//        NLog.LogManager.Shutdown();
//    }
//}
#endregion Logger

