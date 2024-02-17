using System.Data;
using System.Diagnostics;
using MikrotikDotNet;
using MySqlConnector;
using rddish.api.Data;
using rddish.Mikrotik.Services;
using rddish.Radius;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

#region Freeradius configs

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connection = new MySqlConnection(configuration.GetConnectionString("MySQL"));
    return connection;
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

#region Mikrotik configs

builder.Services.AddScoped(opts =>
{
    var mikrotikOptions = configuration.GetSection("Mikrotik").Get<MikrotikOptions>();
    Debug.Assert(mikrotikOptions != null, nameof(mikrotikOptions) + " != null");
    return new MKConnection(mikrotikOptions.Host, mikrotikOptions.User, mikrotikOptions.Password);
});

builder.Services.AddScoped<IMikrotikService, MikrotikService>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Map("/", () => Results.Redirect("/swagger"));

app.Run();