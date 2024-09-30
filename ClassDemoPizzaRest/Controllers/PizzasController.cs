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
            List<IPizza> liste = _repo.GetAll();

            return liste.Count == 0 ? NoContent() : Ok(liste);
        }

        // GET api/<PizzasController>/5
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_repo.GetById(id));
            }catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }

        [HttpGet]
        [Route("byName/{name}")]
        public IActionResult GetByName(string name)
        {

            // hack burde være i repo
            List<IPizza> liste = _repo.GetAll();
            IPizza pizza = liste.Find(p => p.Name.Contains(name));

            return (pizza is null) ? NotFound() : Ok(pizza);

        }

        /*
         * sortering
         */
        // GET: api/<PizzasController>
        [HttpGet]
        [Route("Sort")]
        public IActionResult GetSortet()
        {
            List<IPizza> liste = _repo.GetSortByName();

            return liste.Count == 0 ? NoContent() : Ok(liste);
        }


        /*
         * Filtering
         */
        [HttpGet]
        [Route("Search")]
        public IActionResult GetFilter([FromQuery] FilterDTO filter)
        {
            List<IPizza> liste = _repo.GetByFilter(lowPrice: filter.lowPrice, highPrice: filter.highPrice);

            return liste.Count == 0 ? NoContent() : Ok(liste);
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
