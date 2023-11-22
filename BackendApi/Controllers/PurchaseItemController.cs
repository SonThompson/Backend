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
    public class PurchaseItemController : ControllerBase
    {
        private readonly IPurchaseItemRepo repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public PurchaseItemController(IPurchaseItemRepo repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всей информации о товарах
        /// </summary>
        /// <param name="SortByCount"> сортировка информации о товарах по количеству</param>
        /// <param name="SortByPrice"> сортировка информации о товарах по цене</param>
        /// <returns>Результат</returns>
        [HttpGet("GetAllPurchaseItems")]
        public async Task<ActionResult<List<PurchaseItem>>> GetAllPurchaseItems(bool SortByPrice = false, bool SortByCount = false)
        {
            try
            {
                var result = await repository.AllWithSort(SortByPrice, SortByCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPurchaseItems function error", typeof(PurchaseItemController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Получение информации о товарах
        /// </summary>
        /// <param name="id"> Получение информации о товарах </param>
        /// <returns>Результат</returns>
        [HttpGet("GetPurchaseItemById")]
        public async Task<IActionResult> GetPurchaseItemById(Guid id)
        {
            try
            {
                var result = await repository.GetById(id);

                if (result == null) { return NotFound(); }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPurchaseItems function error", typeof(PurchaseItemController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Удаление информации о товарах
        /// </summary>
        /// <param name="model"> Удаление информации о товарах </param>
        /// <returns>Результат</returns>
        [HttpDelete("DeletePurchaseItem")]
        public async Task<IActionResult> DeletePurchaseItem([FromQuery] ApiGetById model)
        {
            try
            {
                var result = await repository.GetById(model.Id);

                if (result == null) { return BadRequest(); }

                await repository.Delete(model.Id);
                await repository.SaveAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPurchaseItems function error", JsonConvert.SerializeObject(model));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Добавление информации о товарах
        /// </summary>
        /// <param name="purchaseItem"> Добавление информации о товарах </param>
        /// <returns>Результат</returns>
        [HttpPost("AddNewPurchaseItem")]
        public async Task<IActionResult> AddNewPurchaseItem(PurchaseItemModel purchaseItem)
        {
            try
            {
                var entity = mapper.Map<PurchaseItem>(purchaseItem);

                await repository.Add(entity);
                await repository.SaveAsync();

                return Ok(new { Id = entity.Id });//CreatedAtAction("GetItem", new { purchaseItem.Id }, purchaseItem);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPurchaseItems function error", JsonConvert.SerializeObject(purchaseItem));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Обновление информации о товарах
        /// </summary>
        /// <param name="purchaseItem"> Обновление информации о товарах </param>
        /// <returns>Результат</returns>
        [HttpPut("UpdatePurchaseItem")]
        public async Task<IActionResult> UpdatePurchaseItem(PurchaseItem purchaseItem)
        {
            try
            {
                if (purchaseItem.Id != purchaseItem.Id) { return BadRequest(); }

                await repository.Update(purchaseItem);
                await repository.SaveAsync();

                // following up the REST standart on update, we need to return NoContent
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPurchaseItems function error", JsonConvert.SerializeObject(purchaseItem));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
