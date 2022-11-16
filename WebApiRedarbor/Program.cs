using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RedarborDataBase.AdminDB;
using RedarborDataBase.Services;
using RedarBorDB;
using RedarBorModels.AdminDb.Interfaces;
using RedarBorModels.AutoMappings;
using RedarBorModels.Facades.Facade;
using RedarBorModels.Facades.IFacade;
using RedarBorModels.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiRedarBor",
        Version = "v1"
    });

    var securityScheme = new OpenApiSecurityScheme()
    {
        Description = "JWT RedarBorAutentication",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    };

    var securityRequirement = new OpenApiSecurityRequirement
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
        new string[] {}
    }
};

    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
    }
);

var mapperConfig = new MapperConfiguration(m => { m.AddProfile(new AutoMapping()); });
IMapper mapper = mapperConfig.CreateMapper();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RedarBorContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RedarborConnectionDb")));

builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IEmployeeFacade, EmployeeFacade>();
builder.Services.AddScoped<IEmployeeService, EmployeeServices>();
builder.Services.AddScoped<IEmployeeAccesDb, EmployeeAccesDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Redarbor v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
