using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WhatDoWeEat.Api.Controllers
{
    [ApiController]
    [Route("api/custom-meals")]
    public class CustomMealsController : ControllerBase
    {
        private readonly MealPlannerContext _context;

        public CustomMealsController(MealPlannerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<CustomMeal>> CreateCustomMeal(CustomMeal meal)
        {
            _context.CustomMeals.Add(meal);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomMeal), new { id = meal.Id }, meal);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomMeal>>> GetCustomMeals()
        {
            return await _context.CustomMeals.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomMeal>> GetCustomMeal(int id)
        {
            var meal = await _context.CustomMeals.FindAsync(id);
            if (meal == null) return NotFound();
            return meal;
        }
    }
} 