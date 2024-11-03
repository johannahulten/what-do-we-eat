using System.Text.Json;

public class GrocyService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _baseUrl;
    private readonly string _apiKey;

    public GrocyService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _baseUrl = configuration["Grocy:BaseUrl"];
        _apiKey = configuration["Grocy:ApiKey"];
        
        _httpClient.DefaultRequestHeaders.Add("GROCY-API-KEY", _apiKey);
    }

    public async Task<List<GrocyProduct>> GetProducts()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/objects/products");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<GrocyProduct>>(content);
    }

    public async Task<List<GrocyRecipe>> GetRecipes()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/objects/recipes");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<GrocyRecipe>>(content);
    }
}