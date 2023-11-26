using Microsoft.AspNetCore.Mvc;
using BackendHelper.Repositories.IRepos;
using Backend.DAL.RequestModels;
using Newtonsoft.Json;
using Backend.DAL.RequestModels.EntityModels;
using AutoMapper;
using Backend.DAL.Entities;

namespace BackendEntities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepo repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public PurchaseController(IPurchaseRepo repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всей таблицы покупок
        /// </summary>
        /// <returns>Результат</returns>
        [HttpGet("GetAllPurchases")]
        public async Task<ActionResult<List<Purchase>>> GetAllPurchases()
        {
            try
            {
                var result = await repository.All();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPurchases function error", typeof(PurchaseController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Поулчение покупки
        /// </summary>
        /// <param name="id"> идентификатор покупки </param>
        /// <returns>Результат</returns>
        [HttpGet("GetPruchaseById")]
        public async Task<IActionResult> GetPruchaseById(Guid id)
        {
            try
            {
                var result = await repository.GetById(id);

                if (result == null) { return NotFound(); }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPurchases function error", typeof(PurchaseController));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Удаление покупки
        /// </summary>
        /// <param name="model"> идентификатор покупки </param>
        /// <returns>Результат</returns>
        [HttpDelete("DeletePurchase")]
        public async Task<IActionResult> DeletePurchase([FromQuery] ApiGetById model)
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
                logger.LogError(ex, "{Repo} GetAllPurchases function error", JsonConvert.SerializeObject(model));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Добавление покупки
        /// </summary>
        /// <param name="purchase"> параметры покупки </param>
        /// <returns>Результат</returns>
        [HttpPost("AddNewPurchase")]
        public async Task<IActionResult> AddNewPurchase(PurchaseModel purchase)
        {
            try
            {
                var entity = mapper.Map<Purchase>(purchase);

                await repository.Add(entity);
                await repository.SaveAsync();

                return Ok(new { Id = entity.Id });//CreatedAtAction("GetItem", new { purchase.Id }, purchase );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPurchases function error", JsonConvert.SerializeObject(purchase));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Обновление покупки
        /// </summary>
        /// <param name="purchase"> параметры покупки </param>
        /// <returns>Результат</returns>
        [HttpPut("UpdatePurchase")]
        public async Task<IActionResult> UpdatePurchase(Purchase purchase)
        {
            try
            {
                if (purchase.Id != purchase.Id) { return BadRequest(); }

                await repository.Update(purchase);
                await repository.SaveAsync();

                // following up the REST standart on update, we need to return NoContent
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPurchases function error", JsonConvert.SerializeObject(purchase));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
