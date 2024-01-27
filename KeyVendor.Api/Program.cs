using System.Text;
using FluentValidation.AspNetCore;
using KeyVendor.Api.Auth.Extensions;
using KeyVendor.Api.Configuration;
using KeyVendor.Api.Filters;
using KeyVendor.Infrastructure;
using KeyVendor.Application;
using KeyVendor.Domain.Entities;
using KeyVendor.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using RentalCar.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token below.",
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddApplication();
var uploadConfiguration = new UploadConfiguration();
builder.Configuration.GetSection("UploadsConfiguration").Bind(uploadConfiguration);
builder.Services.AddInfrastructure(builder.Configuration);

var mongoDbConfiguration = new MongoDbConfiguration();
builder.Configuration
    .GetSection("MongoDbConfiguration")
    .Bind(mongoDbConfiguration);

builder.Services
    .AddIdentity<User, Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddUserManager<ApplicationUserManager>()
    .AddMongoDbStores<User, Role, ObjectId>(mongoDbConfiguration.ConnectionString,
        mongoDbConfiguration.DatabaseName)
    .AddDefaultTokenProviders()
    .AddPasswordlessLoginTokenProvider();

var jwtConfiguration = new JwtConfiguration();
builder.Configuration
    .GetSection("JwtConfiguration")
    .Bind(jwtConfiguration);
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidAudience = jwtConfiguration.ValidAudience,
            ValidIssuer = jwtConfiguration.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret!))
        };
    });

var corsConfiguration = new CorsConfiguration();

builder.Configuration
    .GetSection("CorsConfiguration")
    .Bind(corsConfiguration);

builder.Services.AddCors(options => options.AddPolicy(ConstantsConfiguration.AllowedOrigins!,
    x => x.WithMethods("GET",
            "POST",
            "PATCH",
            "DELETE",
            "OPTIONS",
            "PUT")
        .WithHeaders(HeaderNames.Accept,
            HeaderNames.ContentType,
            HeaderNames.Authorization,
            HeaderNames.XRequestedWith,
            "x-signalr-user-agent")
        .AllowCredentials()
        .WithOrigins(corsConfiguration.AllowedOrigins!)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(ConstantsConfiguration.AllowedOrigins!);
app.UseAuthorization();
app.MapControllers();
app.Run();