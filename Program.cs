using KafkaExample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Register Kafka producer service with Dependency Injection
builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();

// Add controllers (required for API endpoints)
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer(); // For Swagger UI
builder.Services.AddSwaggerGen(); // For Swagger UI

var app = builder.Build();

// Configure Swagger in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use HTTPS Redirection
app.UseHttpsRedirection();

// Configure the HTTP request pipeline to use controllers
app.MapControllers();

app.Run();