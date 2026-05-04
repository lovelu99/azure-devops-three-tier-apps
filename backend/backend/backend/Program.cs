using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Load Key Vault


try
{
    var keyVaultName = builder.Configuration["KeyVaultName"];
    if (!string.IsNullOrEmpty(keyVaultName))
    {
        var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
        builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Key Vault load failed: {ex.Message}");
}


// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Read connection string from Key Vault secret: SqlConnectionString
var connectionString = builder.Configuration["SqlConnectionString"];
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("SqlConnectionString is missing.");
}


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.MapGet("/health", () => Results.Ok("API healthy"));

app.Run();