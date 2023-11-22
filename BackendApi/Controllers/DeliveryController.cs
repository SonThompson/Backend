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
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryRepo repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public DeliveryController(IDeliveryRepo repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех поставок
        /// </summary>
        /// <param name="SortByCount"> Сортировка полученных категорий по количетсву поставок </param>
        /// <param name="SortByDate"> Сортировка полученных категорий по дате поставки </param>
        /// <returns>Результат</returns>
        [HttpGet("GetAllDeliveries")]
        public async Task<ActionResult<List<Delivery>>> GetAllDeliveries(bool SortByCount, bool SortByDate)
        {
            try
            {
                var result = await repository.AllWithSort(SortByCount, SortByDate);
                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllDeliviries function error", typeof(DeliveryController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Получение поставки
        /// </summary>
        /// <param name="id"> идентификатор поставки </param>
        /// <returns>Результат</returns>
        [HttpGet("GetDeliveryById")]
        public async Task<IActionResult> GetDeliveryById(Guid id)
        {
            try
            {
                var result = await repository.GetById(id);

                if (result == null) { return NotFound(); }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllDeliviries function error", typeof(DeliveryController));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Удаление поставки
        /// </summary>
        /// <param name="model"> идентификатор поставки </param>
        /// <returns>Результат</returns>
        [HttpDelete("DeleteDelivery")]
        public async Task<IActionResult> DeleteDelivery([FromQuery] ApiGetById model)
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
                logger.LogError(ex, "{Repo} GetAllDeliviries function error", JsonConvert.SerializeObject(model));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Добавление поставки
        /// </summary>
        /// <param name="delivery"> данные поставки </param>
        /// <returns>Результат</returns>
        [HttpPost("AddNewDelivery")]
        public async Task<IActionResult> AddNewDelivery(DeliveryModel delivery)
        {
            try
            {
                var entity = mapper.Map<Delivery>(delivery);
                await repository.Add(entity);
                await repository.SaveAsync();

                return Ok(new { Id = entity.Id });//CreatedAtAction("GetItem", new { delivery.Id }, delivery);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllDeliviries function error", JsonConvert.SerializeObject(delivery));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Обновление поставки
        /// </summary>
        /// <param name="delivery"> данные поставки </param>
        /// <returns>Результат</returns>
        [HttpPut("UpdateDelivery")]
        public async Task<IActionResult> UpdateDelivery(Delivery delivery)
        {
            try
            {
                if (delivery.Id != delivery.Id) { return BadRequest(); }

                await repository.Update(delivery);
                await repository.SaveAsync();

                // following up the REST standart on update, we need to return NoContent
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllDeliviries function error", JsonConvert.SerializeObject(delivery));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

