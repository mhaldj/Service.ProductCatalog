using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Service.ProductCatalog.Models;

namespace Service.ProductCatalog.Tests;

public class ProductCategoriesTests
{
    [Fact]
    public async Task TestGetAllEndpoint_NoElements()
    {
        await using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        var response = await client.GetAsync("/ProductCategories");

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task TestCreateProductCategoryEndpoint_SuccessfulCreate()
    {
        await using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        var catName = "Comic";
        var catDescr = "Western style comics";

        var response = await client.PostAsJsonAsync("/ProductCategories", new ProductCategory(catName, catDescr));

        var productCategory = await response.Content.ReadFromJsonAsync<ProductCategory>();

        Assert.NotNull(productCategory);

        Assert.IsType<ProductCategory>(productCategory);
        Assert.Equal(catName, productCategory.Name);
        Assert.Equal(catDescr, productCategory.Description);
    }

}