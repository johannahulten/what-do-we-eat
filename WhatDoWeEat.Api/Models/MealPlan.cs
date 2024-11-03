public record MealPlan
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public MealType MealType { get; set; }
    public string? Notes { get; set; }
    public List<MealItem> MealItems { get; set; } = new();
}

public enum MealType
{
    Breakfast,
    Lunch,
    Snack,
    Dinner
}

public record MealItem
{
    public string Type { get; set; } = string.Empty; // "product", "recipe", or "custom"
    public int Id { get; set; }
    public decimal? Quantity { get; set; }
} 