using Microsoft.AspNetCore.Mvc;
using WebDb.Entities;
using WebDb.Repositories.IPostgres;

namespace WebDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseItemController : ControllerBase
    {
        private readonly IPurchaseItemRepo repository;
        private readonly ILogger logger;
        public PurchaseItemController(IPurchaseItemRepo repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<PurchaseItem>>> GetAllPurchaseItem(bool SortByPrice = false, bool SortByCount = false)
        {
            var result = await repository.AllWithSort(SortByPrice, SortByCount);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPurchaseItemById(Guid id)
        {
            var result = await repository.GetById(id);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePurchaseItem(Guid id)
        {
            var result = await repository.GetById(id);

            if (result == null) { return BadRequest(); }

            await repository.Delete(id);
            await repository.SaveAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPurchaseItem(PurchaseItem purchaseItem)
        {
            if (ModelState.IsValid)
            {
                await repository.Add(purchaseItem);
                await repository.SaveAsync();

                return Ok();//CreatedAtAction("GetItem", new { purchaseItem.Id }, purchaseItem);
            }
            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpsertPurchaseItem(Guid id, PurchaseItem purchaseItem)
        {
            if (id != purchaseItem.Id) { return BadRequest(); }

            await repository.Upsert(purchaseItem);
            await repository.SaveAsync();

            // following up the REST standart on update, we need to return NoContent
            return NoContent();
        }
    }
}
