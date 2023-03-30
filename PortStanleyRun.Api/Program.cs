using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using PortStanleyRun.Api.AuthHandlers;
using PortStanleyRun.Api.Repositories;
using PortStanleyRun.Api.Repositories.Interfaces;
using PortStanleyRun.Api.Services;
using PortStanleyRun.Api.Services.Interfaces;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;
builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SampleAuth0",
        Version = "v1"
    });
    var jwtSecurityScheme = new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token in the text input below.",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = config.GetValue<string>("Auth0:Domain");
    options.Audience = config.GetValue<string>("Auth0:Audience");
});

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("create:users", p => p.Requirements.Add(new HasScopeRequirement("read:runs", config.GetValue<string>("Auth0:Domain"))));
    o.AddPolicy("read:users", p => p.Requirements.Add(new HasScopeRequirement("read:users", config.GetValue<string>("Auth0:Domain"))));
    o.AddPolicy("update:users", p => p.Requirements.Add(new HasScopeRequirement("read:runs", config.GetValue<string>("Auth0:Domain"))));
    o.AddPolicy("delete:users", p => p.Requirements.Add(new HasScopeRequirement("read:runs", config.GetValue<string>("Auth0:Domain"))));
    o.AddPolicy("create:runs", p => p.Requirements.Add(new HasScopeRequirement("read:runs", config.GetValue<string>("Auth0:Domain"))));
    o.AddPolicy("read:runs", p => p.Requirements.Add(new HasScopeRequirement("read:runs", config.GetValue<string>("Auth0:Domain"))));
    o.AddPolicy("update:runs", p => p.Requirements.Add(new HasScopeRequirement("read:runs", config.GetValue<string>("Auth0:Domain"))));
    o.AddPolicy("delete:runs", p => p.Requirements.Add(new HasScopeRequirement("read:runs", config.GetValue<string>("Auth0:Domain"))));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddScoped<IMongoClient>(service => {
    var mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(string.Format(config.GetConnectionString("MongoDb"), config.GetValue<string>("PrimaryPassword"))));
    mongoSettings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
    mongoSettings.ConnectTimeout = TimeSpan.FromSeconds(60);

    return new MongoClient(mongoSettings);
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRunService, RunService>();
builder.Services.AddScoped<IRunRepository, RunRepository>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }