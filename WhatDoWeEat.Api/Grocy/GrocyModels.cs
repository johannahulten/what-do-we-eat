public record GrocyProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public record GrocyRecipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<GrocyProduct> Ingredients { get; set; }
} 