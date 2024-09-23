using ClassDemoPizzaLib.model;
using ClassDemoPizzaLib.Repository;
using ClassDemoPizzaRest.model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassDemoPizzaRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzaRepository _repo;

        public PizzasController(IPizzaRepository repo)
        {
            _repo = repo;
        }





        // GET: api/<PizzasController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        // GET api/<PizzasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_repo.GetById(id));
            }catch (KeyNotFoundException knfe)
            {
                return BadRequest(knfe.Message);
            }
        }

        // POST api/<PizzasController>
        [HttpPost]
        public IActionResult Post([FromBody] PizzaDTO pizzaDto)
        {
            try
            {
                IPizza newPizza = PizzaConverter.PizzaDto2Pizza(pizzaDto);
                return Ok(_repo.Opret(newPizza));
            
            }catch(ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        // PUT api/<PizzasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PizzaDTO pizzaDto)
        {
            try
            {
                IPizza updatePizza = PizzaConverter.PizzaDto2Pizza(pizzaDto);
                return Ok(_repo.Update(id, updatePizza));
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        // DELETE api/<PizzasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_repo.Delete(id));
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }
    }
}
