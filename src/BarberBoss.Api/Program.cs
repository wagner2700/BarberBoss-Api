using BarberBoss.Api.Filters;
using BarberBoss.Api.Midleware;
using BarberBoss.Api.Token;
using BarberBoss.Application;
using BarberBoss.Domain.Users;
using BarberBoss.Infraestructure;
using BarberBoss.Infraestructure.Extension;
using BarberBoss.Infraestructure.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("Bearer" , new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = @"Para inserer Bearer Exemplo : Bearer [espaço] 123455abcdf",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });


    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
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

builder.Services.AddMvc(opt =>
{
    opt.Filters.Add(typeof(ExceptionFilter));
});

builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);

//DependencyInjectionExtension.AddApplication(builder.Services);
builder.Services.AddScoped<ITokenProvider, HttpContextValue>();


builder.Services.AddHttpContextAccessor();
var singninKey = builder.Configuration.GetValue<string>("Settings:jwt:SigninKey");

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = new TimeSpan(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(singninKey!))
    };
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if(builder.Configuration.IsTestEnviroment() == false)
{
    await MigrateDatabase(app);
}



app.UseMiddleware<CultureMidleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
    

app.MapControllers();


app.Run();

async Task MigrateDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    await DataBaseMigration.MigrateDatabase(scope.ServiceProvider);
   
}



public partial class Program{ }

