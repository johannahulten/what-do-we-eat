using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/grocy")]
public class GrocyController : ControllerBase
{
    private readonly GrocyService _grocyService;
    private readonly ILogger<GrocyController> _logger;

    public GrocyController(GrocyService grocyService, ILogger<GrocyController> logger)
    {
        _grocyService = grocyService;
        _logger = logger;
    }

    [HttpGet("products")]
    public async Task<ActionResult<List<GrocyProduct>>> GetProducts()
    {
        try
        {
            var products = await _grocyService.GetProducts();
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching products from Grocy");
            return StatusCode(500, "Error fetching products");
        }
    }

    [HttpGet("recipes")]
    public async Task<ActionResult<List<GrocyRecipe>>> GetRecipes()
    {
        try
        {
            var recipes = await _grocyService.GetRecipes();
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching recipes from Grocy");
            return StatusCode(500, "Error fetching recipes");
        }
    }
}