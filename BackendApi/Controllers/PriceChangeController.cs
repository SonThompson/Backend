using Microsoft.AspNetCore.Mvc;
using BackendHelper.Repositories.IRepos;
using Backend.DAL.RequestModels;
using Newtonsoft.Json;
using Backend.DAL.RequestModels.EntityModels;
using AutoMapper;
using Backend.DAL.Entities;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceChangeController : ControllerBase
    {
        private readonly IPriceChangeRepo repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public PriceChangeController(IPriceChangeRepo repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Поулчение изменения цены
        /// </summary>
        /// <param name="SortByDate"> сортировка изменения цены по дате обновления</param>
        /// <param name="SortByPrice"> сортировка изменения цены по цене</param>
        /// <returns>Результат</returns>
        [HttpGet("GetAllPriceChanges")]
        public async Task<ActionResult<List<PriceChange>>> GetAllPriceChanges(bool SortByDate, bool SortByPrice)
        {
            try
            {
                var result = await repository.AllWithSort(SortByDate, SortByPrice);
                return Ok(result);
            }
            catch (Exception ex)
            {
                    logger.LogError(ex, "{Repo} GetAllPriceChanges function error", typeof(PriceChangeController));
                    return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Получение изменения цены
        /// </summary>
        /// <param name="id"> идентификатор изменения цены </param>
        /// <returns>Результат</returns>
        [HttpGet("GetPriceChangeById")]
        public async Task<IActionResult> GetPriceChangeById(Guid id)
        {
            try
            {
                var result = await repository.GetById(id);

                if (result == null) { return NotFound(); }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPriceChanges function error", typeof(PriceChangeController));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Удаление изменения цены
        /// </summary>
        /// <param name="model"> идентификатор изменения цены </param>
        /// <returns>Результат</returns>
        [HttpDelete("DeletePriceChange")]
        public async Task<IActionResult> DeletePriceChange([FromQuery] ApiGetById model)
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
                logger.LogError(ex, "{Repo} GetAllPriceChanges function error", JsonConvert.SerializeObject(model));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// добавление изменения цены
        /// </summary>
        /// <param name="pc"> параметры изменения цены </param>
        /// <returns>Результат</returns>
        [HttpPost("AddNewPriceChange")]
        public async Task<IActionResult> AddNewPriceChange(PriceChangeModel pc)
        {
            try
            {
                var entity = mapper.Map<PriceChange>(pc);
                await repository.Add(entity);
                await repository.SaveAsync();

                return Ok(new { Id = entity.Id });//CreatedAtAction("GetItem", new { pc.Id }, pc);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPriceChanges function error", JsonConvert.SerializeObject(pc));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Обновление изменения цены
        /// </summary>
        /// <param name="pc"> параметры изменения цены </param>
        /// <returns>Результат</returns>
        [HttpPut("UpdatePriceChange")]
        public async Task<IActionResult> UpdatePriceChange( PriceChange pc)
        {
            try
            {
                if (pc.Id != pc.Id) { return BadRequest(); }

                await repository.Update(pc);
                await repository.SaveAsync();

                // following up the REST standart on update, we need to return NoContent
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllPriceChanges function error", JsonConvert.SerializeObject(pc));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
