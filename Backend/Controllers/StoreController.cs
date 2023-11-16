using Microsoft.AspNetCore.Mvc;
using WebDb.Entities;
using WebDb.Repositories.IPostgres;

namespace WebDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepo repository;
        private readonly ILogger logger;
        public StoreController(IStoreRepo repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Store>>> GetAllStores()
        {
            var result = await repository.All();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStoreById(Guid id)
        {
            var result = await repository.GetById(id);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            var result = await repository.GetById(id);

            if (result == null) { return BadRequest(); }

            await repository.Delete(id);
            await repository.SaveAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStore(Store store)
        {
            if (ModelState.IsValid)
            {
                await repository.Add(store);
                await repository.SaveAsync();

                return Ok();//CreatedAtAction("GetItem", new { store.Id }, store);
            }
            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpsertStore(Guid id, Store store)
        {
            if (id != store.Id) { return BadRequest(); }

            await repository.Upsert(store);
            await repository.SaveAsync();

            // following up the REST standart on update, we need to return NoContent
            return NoContent();
        }
    }
}
