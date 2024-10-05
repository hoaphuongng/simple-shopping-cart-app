using ShoppingCartApplicationBackend.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    //options.AddPolicy("AllowSpecificOrigin",
    //    builder =>
    //    {
    //        builder.WithOrigins("http://localhost:4200")
    //               .AllowAnyMethod()
    //               .AllowAnyHeader();
    //    });
});

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.TypeInfoResolver = AppJsonSerializerContext.Default;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();

[JsonSerializable(typeof(ShoppingCartItem))]
[JsonSerializable(typeof(List<ShoppingCartItem>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}