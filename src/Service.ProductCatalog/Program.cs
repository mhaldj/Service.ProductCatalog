using Microsoft.EntityFrameworkCore;
using Service.ProductCatalog.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductCatalogDataContext>(options => options.UseInMemoryDatabase("ProductCatalog"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
