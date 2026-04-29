using FCG.Infra.Data;
using Microsoft.EntityFrameworkCore;
using FCG.Api.Middlewares;

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
