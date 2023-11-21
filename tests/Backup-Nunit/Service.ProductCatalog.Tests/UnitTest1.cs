using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Net;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Service.ProductCatalog.Tests;

public class ProductCatalogApiTests
{
    private readonly HttpClient _httpClient;

    public ProductCatalogApiTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}