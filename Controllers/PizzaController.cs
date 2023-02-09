using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class PizzaController:ControllerBase {
    public PizzaController() {

    }
    /// <summary>
    /// GetAll
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="id">id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id) {
        var pizza = PizzaService.Get(id);

        if(pizza == null) {
            return NotFound();
        }

        return pizza;
    }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="pizza">pizza</param>
    /// <returns></returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     POST /Todo
    ///     {
    ///        "id": 1,
    ///        "name": "Item #1",
    ///        "isGlutenFree": true
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    public IActionResult Create(Pizza pizza) {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new {id = pizza.Id}, pizza);
        // return CreatedAtAction("created", pizza);
        // return BadRequest();
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="pizza">pizza</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza) {
        if(id != pizza.Id) {
            return BadRequest();
        }

        var existingPizza = PizzaService.Get(id);
        if(existingPizza is null) {
            return NotFound();
        }

        PizzaService.Update(pizza);
        return NoContent();
    }
    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id">id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        var pizza = PizzaService.Get(id);
         if(pizza is null) {
            return NotFound();
         }

         PizzaService.Delete(id);
         return NoContent();
    }
}