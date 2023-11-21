using Service.ProductCatalog.DataAccess;
using Microsoft.EntityFrameworkCore;
using Service.ProductCatalog.Models;
using Service.ProductCatalog.Features;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductCatalogDataContext>(options => options.UseInMemoryDatabase("ProductCatalog"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddProductFeaturesPostEndpoints();
app.AddProductFeaturesPutEndpoints();
app.AddProductFeaturesGetEndpoints();
app.AddProductCategoryFeaturesEndpoints();

app.UseHttpsRedirection();

app.Run();

public partial class Program { }

