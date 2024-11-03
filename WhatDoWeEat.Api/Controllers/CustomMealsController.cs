using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WhatDoWeEat.Api.Controllers
{
    [ApiController]
    [Route("api/custom-meals")]
    public class CustomMealsController : ControllerBase
    {
        private readonly IRepository<CustomMeal> _repository;
        private readonly ILogger<CustomMealsController> _logger;

        public CustomMealsController(IRepository<CustomMeal> repository, ILogger<CustomMealsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomMeal>>> GetCustomMeals()
        {
            try
            {
                var meals = await _repository.GetAllAsync();
                return Ok(meals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving custom meals");
                return StatusCode(500, "An error occurred while retrieving custom meals");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomMeal>> GetCustomMeal(int id)
        {
            try
            {
                var meal = await _repository.GetByIdAsync(id);
                if (meal == null) return NotFound();
                return Ok(meal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving custom meal");
                return StatusCode(500, "An error occurred while retrieving the custom meal");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomMeal>> CreateCustomMeal(CustomMeal meal)
        {
            try
            {
                var created = await _repository.AddAsync(meal);
                return CreatedAtAction(nameof(GetCustomMeal), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating custom meal");
                return StatusCode(500, "An error occurred while creating the custom meal");
            }
        }
    }
} 