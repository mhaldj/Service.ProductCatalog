using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Service.ProductCatalog.Models;

public class ProductCategory(string name, string description)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string? Name { get; set; } = name;

    public string? Description { get; set; } = description;

    [JsonIgnore]
    public virtual IEnumerable<Product> Products { get; } = new List<Product>();
}
