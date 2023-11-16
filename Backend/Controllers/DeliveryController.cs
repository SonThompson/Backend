using Microsoft.AspNetCore.Mvc;
using WebDb.Entities;
using WebDb.Repositories.IPostgres;

namespace WebDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryRepo repository;
        private readonly ILogger logger;
        public DeliveryController(IDeliveryRepo repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Delivery>>> GetAllDeliveries(bool SortByCount, bool SortByDate)
        {
            var result = await repository.AllWithSort(SortByCount, SortByDate);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDeliveryById(Guid id)
        {
            var result = await repository.GetById(id);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDelivery(Guid id)
        {
            var result = await repository.GetById(id);

            if (result == null) { return BadRequest(); }

            await repository.Delete(id);
            await repository.SaveAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewDelivery(Delivery delivery)
        {
            if (ModelState.IsValid)
            {

                await repository.Add(delivery);
                await repository.SaveAsync();

                return Ok();//CreatedAtAction("GetItem", new { delivery.Id }, delivery);
            }
            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpsertDelivery(Guid id, Delivery delivery)
        {
            if (id != delivery.Id) { return BadRequest(); }

            await repository.Upsert(delivery);
            await repository.SaveAsync();

            // following up the REST standart on update, we need to return NoContent
            return NoContent();
        }
    }
}

