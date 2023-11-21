using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Service.ProductCatalog.Models;

namespace Service.ProductCatalog.Tests;

public class ProductGetAndCreateMultipleTests
{
    [Fact]
    public async Task TestCreateAndGetProductEndpoint_CreateAndGetMultiple()
    {
        await using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        var prod1Name = "The Incredible Hulk";
        var prod1Descr = "Latest issue";
        var prod1Price = new decimal(54.95);

        var createResponse1 = await client.PostAsJsonAsync("/Products", new Product(prod1Name, prod1Descr, prod1Price, null));
        var createdProduct1 = await createResponse1.Content.ReadFromJsonAsync<Product>();

        Assert.NotNull(createdProduct1);

        var prod2Name = "Thor";
        var prod2Descr = "Latest issue";
        var prod2Price = new decimal(54.95);

        var createResponse2 = await client.PostAsJsonAsync("/Products", new Product(prod2Name, prod2Descr, prod2Price, null));
        var createdProduct2 = await createResponse2.Content.ReadFromJsonAsync<Product>();

        Assert.NotNull(createdProduct2);

        var getAllResponse = await client.GetAsync($"/Products");

        var getAllProducts = await getAllResponse.Content.ReadFromJsonAsync<List<Product>>();

        Assert.NotNull(getAllProducts);
        Assert.All(getAllProducts, item => Assert.IsType<Product>(item));
        Assert.Contains(getAllProducts, item => item.Id == createdProduct1.Id);
        Assert.Contains(getAllProducts, item => item.Id == createdProduct2.Id);
    }
}