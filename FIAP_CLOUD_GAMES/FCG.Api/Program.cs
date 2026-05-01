using FCG.Api.Middlewares;
using FCG.Application.Handlers;
using FCG.Application.Interfaces;
using FCG.Domain.Interfaces.Repositories;
using FCG.Domain.Interfaces.Services;
using FCG.Domain.Mappings;
using FCG.Domain.Services;
using FCG.Infra.Data;
using FCG.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


#region Auth
    var chaveJwt = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(chaveJwt),

            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("JWT ERROR:");
                Console.WriteLine(context.Exception.ToString());

                return Task.CompletedTask;
            },

            OnChallenge = context =>
            {
                Console.WriteLine("JWT CHALLENGE:");
                Console.WriteLine(context.Error);
                Console.WriteLine(context.ErrorDescription);

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
#endregion

#region Swagger

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Insira o token JWT. Exemplo: Bearer {seu_token}"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
    });

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

    builder.Services.AddScoped<IAuthHandler, AuthHandler>();
    builder.Services.AddScoped<IAuthService, AuthService>();

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


app.UseHttpsRedirection();

app.UseMiddleware<TratamentoErroMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
