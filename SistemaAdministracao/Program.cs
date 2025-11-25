using Microsoft.EntityFrameworkCore;
using SistemaAdministracao.Data;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var rawConnection = builder.Configuration.GetConnectionString("Postgres");

var senha = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString = rawConnection.Replace("__DB_PASSWORD__", senha);

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
