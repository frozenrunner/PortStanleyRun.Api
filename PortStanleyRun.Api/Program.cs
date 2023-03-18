using MongoDB.Driver;
using PortStanleyRun.Api.Services;
using PortStanleyRun.Api.Services.Interfaces;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sg =>
{
    sg.EnableAnnotations();
});

builder.Services.AddScoped<IMongoClient>(service => {
    var mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(string.Format(config.GetConnectionString("MongoDb"), config.GetValue<string>("PrimaryPassword"))));
    mongoSettings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
    mongoSettings.ConnectTimeout = TimeSpan.FromSeconds(60);

    return new MongoClient(mongoSettings);
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRunService, RunService>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
