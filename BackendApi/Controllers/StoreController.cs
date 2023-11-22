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
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepo repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public StoreController(IStoreRepo repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение филиалов
        /// </summary>
        /// <returns>Результат</returns>
        [HttpGet("GetAllStores")]
        public async Task<ActionResult<List<Store>>> GetAllStores()
        {
            try
            {
                var result = await repository.All();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllStores function error", typeof(StoreController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Получение филиала
        /// </summary>
        /// <param name="id"> получение филиала </param>
        /// <returns>Результат</returns>
        [HttpGet("GetStoreById")]
        public async Task<IActionResult> GetStoreById(Guid id)
        {
            try
            {
                var result = await repository.GetById(id);

                if (result == null) { return NotFound(); }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllStores function error", typeof(StoreController));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Удаление филиала
        /// </summary>
        /// <param name="model"> удаление филиала </param>
        /// <returns>Результат</returns>
        [HttpDelete("DeleteStore")]
        public async Task<IActionResult> DeleteStore([FromQuery] ApiGetById model)
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
                logger.LogError(ex, "{Repo} GetAllStores function error", JsonConvert.SerializeObject(model));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Добавление филиала
        /// </summary>
        /// <param name="store"> добавление филиала </param>
        /// <returns>Результат</returns>
        [HttpPost("AddNewStore")]
        public async Task<IActionResult> AddNewStore(Store store)
        {
            try 
            {
                var entity = mapper.Map<Store>(store);

                await repository.Add(entity);
                await repository.SaveAsync();

                return Ok(new { Id = entity.Id });//CreatedAtAction("GetItem", new { store.Id }, store);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllStores function error", JsonConvert.SerializeObject(store));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Обновление филиала
        /// </summary>
        /// <param name="store"> обновление филиала </param>
        /// <returns>Результат</returns>
        [HttpPut("UpdateStore")]
        public async Task<IActionResult> UpdateStore(Store store)
        {
            try
            {
                if (store.Id != store.Id) { return BadRequest(); }

                await repository.Update(store);
                await repository.SaveAsync();

                // following up the REST standart on update, we need to return NoContent
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllStores function error", JsonConvert.SerializeObject(store));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
