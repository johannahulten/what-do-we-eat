using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class MealPlannerContext : DbContext
{
    public MealPlannerContext(DbContextOptions<MealPlannerContext> options)
        : base(options)
    {
    }

    public DbSet<CustomMeal> CustomMeals { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MealPlan>()
            .Property(e => e.MealItems)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                v => JsonSerializer.Deserialize<List<MealItem>>(v, (JsonSerializerOptions)null!)!);

        modelBuilder.Entity<MealPlan>()
            .Property(e => e.MealType)
            .HasConversion<string>();
    }
} 