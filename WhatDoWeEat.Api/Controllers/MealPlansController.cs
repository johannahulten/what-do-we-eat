using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WhatDoWeEat.Api.Controllers
{
    [ApiController]
    [Route("api/meal-plans")]
    public class MealPlansController : ControllerBase
    {
        private readonly MealPlannerContext _context;

        public MealPlansController(MealPlannerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<MealPlan>> CreateMealPlan(MealPlan plan)
        {
            _context.MealPlans.Add(plan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMealPlan), new { id = plan.Id }, plan);
        }

        [HttpGet("week/{weekNumber}")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<MealPlan>> GetMealPlan(int id)
        {
            var plan = await _context.MealPlans.FindAsync(id);
            if (plan == null) return NotFound();
            return plan;
        }

        [HttpDelete("{id}")]
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