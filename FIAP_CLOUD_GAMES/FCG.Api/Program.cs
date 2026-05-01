using FCG.Api.Middlewares;
using FCG.Application.Handlers;
using FCG.Application.Interfaces;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Interfaces.Services;
using FCG.Domain.Mappings;
using FCG.Domain.Services;
using FCG.Infra.Data;
using FCG.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Swagger

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

#endregion

#region Entity Framework

    builder.Services.AddDbContext<FCGDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
        ));

#endregion

#region Mapper

    builder.Services.AddAutoMapper(typeof(JogoProfile).Assembly);

#endregion

#region DI

    builder.Services.AddScoped<IJogoService, JogoService>();
    builder.Services.AddScoped<IJogoHandler, JogoHandler>();
    builder.Services.AddScoped<IJogoRepository, JogoRepository>();

    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    builder.Services.AddScoped<IUsuarioHandler, UsuarioHandler>();
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TratamentoErroMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
