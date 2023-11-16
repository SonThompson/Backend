using Microsoft.AspNetCore.Mvc;
using WebDb.Entities;
using WebDb.Repositories.IPostgres;

namespace WebDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceChangeController : ControllerBase
    {
        private readonly IPriceChangeRepo repository;
        private readonly ILogger logger;
        public PriceChangeController(IPriceChangeRepo repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<PriceChange>>> GetAllPriceChanges(bool SortByDate, bool SortByPrice)
        {
            var result = await repository.AllWithSort(SortByDate, SortByPrice);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPriceChangeById(Guid id)
        {
            var result = await repository.GetById(id);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePriceChange(Guid id)
        {
            var result = await repository.GetById(id);

            if (result == null) { return BadRequest(); }

            await repository.Delete(id);
            await repository.SaveAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPriceChange(PriceChange pc)
        {
            if (ModelState.IsValid)
            {

                await repository.Add(pc);
                await repository.SaveAsync();

                return Ok();//CreatedAtAction("GetItem", new { pc.Id }, pc);
            }
            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpsertPriceChange(Guid id, PriceChange pc)
        {
            if (id != pc.Id) { return BadRequest(); }

            await repository.Upsert(pc);
            await repository.SaveAsync();

            // following up the REST standart on update, we need to return NoContent
            return NoContent();
        }
    }
}
