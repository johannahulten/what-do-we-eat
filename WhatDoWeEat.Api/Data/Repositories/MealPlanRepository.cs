using Microsoft.EntityFrameworkCore;

namespace WhatDoWeEat.Api.Data.Repositories
{
    public class MealPlanRepository : IRepository<MealPlan>
    {
        private readonly MealPlannerContext _context;

        public MealPlanRepository(MealPlannerContext context)
        {
            _context = context;
        }

        public async Task<MealPlan?> GetByIdAsync(int id)
        {
            return await _context.MealPlans.FindAsync(id);
        }

        public async Task<IEnumerable<MealPlan>> GetAllAsync()
        {
            return await _context.MealPlans.ToListAsync();
        }

        public async Task<IEnumerable<MealPlan>> GetByWeekAsync(int weekNumber)
        {
            var startDate = ISOWeek.ToDateTime(DateTime.Now.Year, weekNumber, DayOfWeek.Monday);
            var endDate = startDate.AddDays(7);

            return await _context.MealPlans
                .Where(p => p.Date >= startDate && p.Date < endDate)
                .OrderBy(p => p.Date)
                .ThenBy(p => p.MealType)
                .ToListAsync();
        }

        public async Task<MealPlan> AddAsync(MealPlan entity)
        {
            _context.MealPlans.Add(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(MealPlan entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(MealPlan entity)
        {
            _context.MealPlans.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
} 