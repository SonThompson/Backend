using Microsoft.AspNetCore.Mvc;
using BackendHelper.Repositories.IRepos;
using BackendEntities.RequestModels;
using Newtonsoft.Json;
using BackendEntities.RequestModels.EntityModels;
using AutoMapper;
using BackendEntities.Entities;

namespace BackendEntities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public ProductController(IProductRepo repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }
        /// <summary>
        /// получение всех продуктов
        /// </summary>
        /// <returns>Результат</returns>
        [HttpGet("GetAllProduct")]
        public async Task<ActionResult<List<Product>>> GetAllProduct()
        {
            try
            {
                var result = await repository.All();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllProduct function error", typeof(ProductController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Получение продукта
        /// </summary>
        /// <param name="id"> идентификатор продукта </param>
        /// <returns>Результат</returns>
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var result = await repository.GetById(id);

                if (result == null) { return NotFound(); }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllProduct function error", typeof(ProductController));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Удаление продукта
        /// </summary>
        /// <param name="model"> идентификатор продукта </param>
        /// <returns>Результат</returns>
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] ApiGetById model)
        {
            try
            {
                var result = await repository.GetById(model.Id);

                if (result == null) { return BadRequest(); }

                await repository.Delete(model.Id);
                await repository.SaveAsync();

                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllProduct function error", JsonConvert.SerializeObject(model));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Добавление продукта
        /// </summary>
        /// <param name="product"> параметры продукта </param>
        /// <returns>Результат</returns>
        [HttpPost("AddNewProduct")]
        public async Task<IActionResult> AddNewProduct(ProductModel product)
        {
            try
            {
                var entity = mapper.Map<Product>(product);

                await repository.Add(entity);
                await repository.SaveAsync();

                return Ok(new { Id = entity.Id });//CreatedAtAction("GetItem", new { product.Id }, product);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllProduct function error", JsonConvert.SerializeObject(product));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Обновление продукта
        /// </summary>
        /// <param name="product"> параметры продукта </param>
        /// <returns>Результат</returns>
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                if (product.Id != product.Id) { return BadRequest(); }

                await repository.Update(product);
                await repository.SaveAsync();

                // following up the REST standart on update, we need to return NoContent
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllProduct function error", JsonConvert.SerializeObject(product));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
