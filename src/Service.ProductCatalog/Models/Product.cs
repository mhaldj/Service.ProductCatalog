using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.ProductCatalog.Models;

public class Product(string name, string description, decimal? price, Guid? productCategoryId)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string? Name { get; set; } = name;
    public string? Description { get; set; } = description;
    public decimal? Price { get; set; } = price;
    public Guid? ProductCategoryId { get; set; } = productCategoryId;
    public ProductCategory? ProductCategory { get; init; } = null!;
}
