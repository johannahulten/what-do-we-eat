using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WhatDoWeEat.Api.Controllers
{
    [ApiController]
    [Route("api/meal-plans")]
    public class MealPlansController : ControllerBase
    {
        private readonly IRepository<MealPlan> _repository;
        private readonly ILogger<MealPlansController> _logger;

        public MealPlansController(IRepository<MealPlan> repository, ILogger<MealPlansController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<MealPlan>> CreateMealPlan(MealPlan plan)
        {
            try
            {
                var created = await _repository.AddAsync(plan);
                return CreatedAtAction(nameof(GetMealPlan), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating meal plan");
                return StatusCode(500, "An error occurred while creating the meal plan");
            }
        }

        [HttpGet("week/{weekNumber}")]
        public async Task<ActionResult<IEnumerable<MealPlan>>> GetMealPlansForWeek(int weekNumber)
        {
            try
            {
                var repository = (_repository as MealPlanRepository) 
                    ?? throw new InvalidOperationException("Invalid repository type");
                
                var plans = await repository.GetByWeekAsync(weekNumber);
                return Ok(plans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving meal plans for week {WeekNumber}", weekNumber);
                return StatusCode(500, "An error occurred while retrieving meal plans");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MealPlan>> GetMealPlan(int id)
        {
            try
            {
                var plan = await _repository.GetByIdAsync(id);
                if (plan == null) return NotFound();
                return Ok(plan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving meal plan {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the meal plan");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMealPlan(int id)
        {
            try
            {
                var plan = await _repository.GetByIdAsync(id);
                if (plan == null) return NotFound();

                await _repository.DeleteAsync(plan);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting meal plan {Id}", id);
                return StatusCode(500, "An error occurred while deleting the meal plan");
            }
        }
    }
} 