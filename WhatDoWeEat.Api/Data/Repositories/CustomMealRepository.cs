using Microsoft.EntityFrameworkCore;

public class CustomMealRepository : IRepository<CustomMeal>
{
    private readonly MealPlannerContext _context;

    public CustomMealRepository(MealPlannerContext context)
    {
        _context = context;
    }

    public async Task<CustomMeal?> GetByIdAsync(int id)
    {
        return await _context.CustomMeals.FindAsync(id);
    }

    public async Task<IEnumerable<CustomMeal>> GetAllAsync()
    {
        return await _context.CustomMeals.ToListAsync();
    }

    public async Task<CustomMeal> AddAsync(CustomMeal entity)
    {
        _context.CustomMeals.Add(entity);
        await SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(CustomMeal entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(CustomMeal entity)
    {
        _context.CustomMeals.Remove(entity);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
} 