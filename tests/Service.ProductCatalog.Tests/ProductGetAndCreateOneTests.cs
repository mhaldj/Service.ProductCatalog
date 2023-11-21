using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Service.ProductCatalog.Models;

namespace Service.ProductCatalog.Tests;

public class ProductGetAndCreateOneTests
{
    [Fact]
    public async Task TestCreateProductEndpoint_CreateAndGetOne()
    {
        await using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        var prodName = "Batman";
        var prodDescr = "Latest issue";
        var prodPrice = new decimal(54.95);

        var response = await client.PostAsJsonAsync("/Products", new Product(prodName, prodDescr, prodPrice, null));

        var product = await response.Content.ReadFromJsonAsync<Product>();

        Assert.NotNull(product);

        var requestUri = "/Products/" + product.Id.ToString();

        var getResponse = await client.GetAsync(requestUri);
        var getProduct = await getResponse.Content.ReadFromJsonAsync<Product>();

        Assert.NotNull(getProduct);
        Assert.Equal(product.Id, getProduct.Id);
    }
}