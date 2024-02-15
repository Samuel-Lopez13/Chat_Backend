using System.Reflection;
using Core.Domain.Services;
using Microsoft.OpenApi.Models;
using Presentation.Filters;
using Presentation.Services;

namespace Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo 
            {
                Title = "ChatApp", 
                Version = "v1" 
            });
    
            c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Porfavor inserta tu JWT Bearer en este campo",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
            
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            //Genera los documentos
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Presentation.xml"));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Core.xml"));
        });

        //Agrega el HTTPContext y este sirve para poder obtener el usuario
        services.AddHttpContextAccessor();
        services.AddTransient<IAuthorization, Authorization>();

        //Genera las excepciones
        services.AddControllers(options => 
            options.Filters.Add<ApiExceptionFilterAttribute>()).AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter())
        );

        return services;
    }
}