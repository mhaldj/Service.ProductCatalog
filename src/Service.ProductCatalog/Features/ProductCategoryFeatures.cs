using Service.ProductCatalog.DataAccess;
using Microsoft.EntityFrameworkCore;
using Service.ProductCatalog.Models;

namespace Service.ProductCatalog.Features;

public static class ProductCategoryFeatures
{
    public static void AddProductCategoryFeaturesEndpoints(this IEndpointRouteBuilder app)
    {
        // Add New ProductCategory
        app.MapPost("/ProductCategories", async (ProductCategory productCategory, ProductCatalogDataContext db) =>
        {
            db.ProductCategories.Add(productCategory);
            await db.SaveChangesAsync();

            return Results.Created($"/save/{productCategory.Id}", productCategory);
        });

        // GET
        //Get all product categories
        app.MapGet("/ProductCategories", async (ProductCatalogDataContext db) => await db.ProductCategories.Include(pc => pc.Products).ToListAsync());

        // Get product category by Id
        app.MapGet("/ProductCategories/{id}", async (Guid id, ProductCatalogDataContext db) =>
            await db.ProductCategories.FindAsync(id)
                is ProductCategory productCategory
                ? Results.Ok(productCategory)
                : Results.NotFound());

    }
}
