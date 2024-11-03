using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WhatDoWeEat.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class MealPlannerController : ControllerBase
    {
        private readonly MealPlannerContext _context;

        public MealPlannerController(MealPlannerContext context)
        {
            _context = context;
        }

        [HttpPost("custom-meals")]
        public async Task<ActionResult<CustomMeal>> CreateCustomMeal(CustomMeal meal)
        {
            _context.CustomMeals.Add(meal);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomMeal), new { id = meal.Id }, meal);
        }

        [HttpGet("custom-meals")]
        public async Task<ActionResult<IEnumerable<CustomMeal>>> GetCustomMeals()
        {
            return await _context.CustomMeals.ToListAsync();
        }

        [HttpGet("custom-meals/{id}")]
        public async Task<ActionResult<CustomMeal>> GetCustomMeal(int id)
        {
            var meal = await _context.CustomMeals.FindAsync(id);
            if (meal == null) return NotFound();
            return meal;
        }

        [HttpPost("meal-plans")]
        public async Task<ActionResult<MealPlan>> CreateMealPlan(MealPlan plan)
        {
            _context.MealPlans.Add(plan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMealPlan), new { id = plan.Id }, plan);
        }

        [HttpGet("meal-plans/{weekNumber}")]
        public async Task<ActionResult<IEnumerable<MealPlan>>> GetMealPlansForWeek(int weekNumber)
        {
            var startDate = ISOWeek.ToDateTime(DateTime.Now.Year, weekNumber, DayOfWeek.Monday);
            var endDate = startDate.AddDays(7);

            return await _context.MealPlans
                .Where(p => p.Date >= startDate && p.Date < endDate)
                .OrderBy(p => p.Date)
                .ThenBy(p => p.MealType)
                .ToListAsync();
        }

        [HttpGet("meal-plans/{id}")]
        public async Task<ActionResult<MealPlan>> GetMealPlan(int id)
        {
            var plan = await _context.MealPlans.FindAsync(id);
            if (plan == null) return NotFound();
            return plan;
        }

        [HttpDelete("meal-plans/{id}")]
        public async Task<IActionResult> DeleteMealPlan(int id)
        {
            var plan = await _context.MealPlans.FindAsync(id);
            if (plan == null) return NotFound();

            _context.MealPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
