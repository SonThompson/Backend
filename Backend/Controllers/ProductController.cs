using Microsoft.AspNetCore.Mvc;
using WebDb.Entities;
using WebDb.Repositories.IPostgres;

namespace WebDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo repository;
        private readonly ILogger logger;
        public ProductController(IProductRepo repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProduct()
        {
            var result = await repository.All();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var result = await repository.GetById(id);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await repository.GetById(id);

            if (result == null) { return BadRequest(); }

            await repository.Delete(id);
            await repository.SaveAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await repository.Add(product);
                await repository.SaveAsync();

                return Ok();//CreatedAtAction("GetItem", new { product.Id }, product);
            }
            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpsertProduct(Guid id, Product product)
        {
            if (id != product.Id) { return BadRequest(); }

            await repository.Upsert(product);
            await repository.SaveAsync();

            // following up the REST standart on update, we need to return NoContent
            return NoContent();
        }
    }
}
