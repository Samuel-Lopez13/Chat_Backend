using Core.Domain.Entities;
using Core.Domain.Services;
using Core.Features.Usuario.Command;
using Core.Infraestructure;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presentation;
using Presentation.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddMediatR(typeof(Usuario).Assembly);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddSecurity(builder.Configuration);

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy", builder =>
    {
        builder.WithOrigins("https://chatearapp.netlify.app/") // Reemplaza con el origen de tu aplicación Vue.js
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Permitir credenciales
    });
});*/

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("https://chatearapp.netlify.app")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

//Database
const string connectionName = "ConexionMaestra";
var connectionString = builder.Configuration.GetConnectionString(connectionName);
builder.Services.AddDbContext<ChatContext>(options => options.UseMySQL(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/message");
    endpoints.MapControllers();
});

app.UseHttpsRedirection();
app.UseRouting(); 
app.UseCors("AllowOrigin");
app.UseAuthorization();
//app.UseCors("SignalRPolicy");

//app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
