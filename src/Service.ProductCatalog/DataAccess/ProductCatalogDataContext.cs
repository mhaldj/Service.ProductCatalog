using Microsoft.EntityFrameworkCore;
using Service.ProductCatalog.Models;

namespace Service.ProductCatalog.DataAccess;

public class ProductCatalogDataContext(DbContextOptions<ProductCatalogDataContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
}
