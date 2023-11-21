using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Service.ProductCatalog.Models;

namespace Service.ProductCatalog.Tests;

public class ProductTests
{
    [Fact]
    public async Task TestGetAllEndpoint()
    {
        await using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        var response = await client.GetAsync("/Products");

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task TestCreateProductEndpoint_SuccessfulCreate()
    {
        await using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        var prodName = "Batman";
        var prodDescr = "Latest issue";
        var prodPrice = new decimal(54.95);

        var response = await client.PostAsJsonAsync("/Products", new Product(prodName, prodDescr, prodPrice, null));

        var product = await response.Content.ReadFromJsonAsync<Product>();

        Assert.IsType<Product>(product);
        Assert.Equal(prodName, product.Name);
        Assert.Equal(prodDescr, product.Description);
        Assert.Equal(prodPrice, product.Price);
    }

    [Fact]
    public async Task TestCreateProductEndpoint_InvalidProductCategory()
    {
        await using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        var prodName = "Superman";
        var prodDescr = "Latest issue";
        var prodPrice = new decimal(64.95);
        var prodCategoryId = Guid.NewGuid();

        var response = await client.PostAsJsonAsync("/Products", new Product(prodName, prodDescr, prodPrice, prodCategoryId));

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}