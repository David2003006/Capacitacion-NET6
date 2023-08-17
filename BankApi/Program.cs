using BankApi.Data;
using BankApi.Services;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("BankConection");
if (!string.IsNullOrEmpty(connectionString))
{
    // Aquí puedes usar la cadena de conexión de manera segura
    builder.Services.AddDbContext<BankContext>(options =>
    {
        options.UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(8, 0, 19))
        );
    });
}

builder.Services.AddScoped<AccountServices>();
builder.Services.AddScoped<ClientServices>();
builder.Services.AddScoped<AccountTypeServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
