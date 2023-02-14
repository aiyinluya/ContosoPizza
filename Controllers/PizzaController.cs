using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Produces("application/json")]
// [EnableCors("myCors")] //允许跨域,可以不在这里写
// [EnableCors(origins: "http://localhost:8081/", headers: "*", methods: "GET,POST,PUT,DELETE")]
public class PizzaController:ControllerBase {
    public PizzaController() {

    }
    [HttpGet]
    public ActionResult<String> SGet() => "Hello World！";
    /// <summary>
    /// 获取全部信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAll")]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    /// <summary>
    /// 获取指定信息
    /// </summary>
    /// <param name="id">唯一id</param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<Pizza> Get(int id) {
        var pizza = PizzaService.Get(id);

        if(pizza == null) {
            return NotFound();
        }

        return pizza;
    }

    /// <summary>
    /// 添加一条新内容
    /// </summary>
    /// <param name="pizza">内容</param>
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
    [HttpPost("Create(pizza)")]
    public IActionResult Create(Pizza pizza) {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new {id = pizza.Id}, pizza);
        // return CreatedAtAction("created", pizza);
        // return BadRequest();
    }

    /// <summary>
    /// 修改已经存在的内容
    /// </summary>
    /// <param name="id">唯一id</param>
    /// <param name="pizza">修改的完整内容</param>
    /// <returns></returns>
    [HttpPut("Update(id, pizza)")]
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
    /// 删除指定内容
    /// </summary>
    /// <param name="id">唯一id</param>
    /// <returns></returns>
    [HttpDelete("Delete(id)")]
    public IActionResult Delete(int id) {
        var pizza = PizzaService.Get(id);
         if(pizza is null) {
            return NotFound();
         }

         PizzaService.Delete(id);
         return NoContent();
    }
}