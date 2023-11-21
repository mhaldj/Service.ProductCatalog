using Service.ProductCatalog.DataAccess;
using Microsoft.EntityFrameworkCore;
using Service.ProductCatalog.Models;
using System.Text.Json.Serialization;

namespace Service.ProductCatalog.Features;

public static class ProductFeatures
{
    public static void AddProductFeaturesPostEndpoints(this IEndpointRouteBuilder app)
    {
        // Add New Product
        app.MapPost("/Products", async (Product product, ProductCatalogDataContext db) =>
        {
            if (product.ProductCategoryId != null)
            {
                var productCategory = await db.ProductCategories.FindAsync(product.ProductCategoryId);
                if (productCategory is null) return Results.NotFound("Invalid product category");
            }
            db.Products.Add(product);
            await db.SaveChangesAsync();

            return Results.Created($"/save/{product.Id}", product);
        });
    }

    public static void AddProductFeaturesPutEndpoints(this IEndpointRouteBuilder app)
    {
        // Update existing product
        app.MapPut("/Products/{id}", async (Guid id, Product updateProduct, ProductCatalogDataContext db) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound();

            if (updateProduct.Name != null) product.Name = updateProduct.Name;
            if (updateProduct.Description != null) product.Description = updateProduct.Description;
            if (updateProduct.Price != null) product.Price = updateProduct.Price;

            if (updateProduct.ProductCategoryId != null)
            {
                var productCategory = await db.ProductCategories.FindAsync(updateProduct.ProductCategoryId);
                if (productCategory is null) return Results.NotFound("Invalid product category");
                product.ProductCategoryId = updateProduct.ProductCategoryId;
            }

            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }

    public static void AddProductFeaturesGetEndpoints(this IEndpointRouteBuilder app)
    {
        // Get all products
        app.MapGet("/Products", async (ProductCatalogDataContext db) => await db.Products.Include(p => p.ProductCategory).ToListAsync());

        // Get product by Id
        app.MapGet("/Products/{id}", async (Guid id, ProductCatalogDataContext db) =>
            await db.Products.Include(p => p.ProductCategory).FirstOrDefaultAsync(e => e.Id == id)
                is Product product
                ? Results.Ok(product)
                : Results.NotFound());

        // Get all products by ProductCategoryId
        app.MapGet("/Products/ProductCategory/{pcid}", async (Guid pcid, ProductCatalogDataContext db) =>
            await db.Products.Include(p => p.ProductCategory).Where(e => e.ProductCategoryId == pcid).ToListAsync());
    }
}