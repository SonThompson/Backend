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
    public class ManufactureController : ControllerBase
    {
        private readonly IManufactureRepo repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public ManufactureController(IManufactureRepo repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// получение всех мануфактур
        /// </summary>
        /// <returns>Результат</returns>
        [HttpGet("GetAllManufactures")]
        public async Task<ActionResult<List<Manufacture>>> GetAllManufactures()
        {
            try
            {
                var result = await repository.All();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllManufacture function error", typeof(ManufactureController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Поулчение мануфактуры
        /// </summary>
        /// <param name="id"> идентификатор мануфактуры </param>
        /// <returns>Результат</returns>
        [HttpGet("GetManufactureById")]
        public async Task<IActionResult> GetManufactureById(Guid id)
        {
            try
            {
                var result = await repository.GetById(id);

                if (result == null) { return NotFound(); }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllManufacture function error", typeof(ManufactureController));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Удаление мануфактуры
        /// </summary>
        /// <param name="model"> идентификатор мануфактуры </param>
        /// <returns>Результат</returns>
        [HttpDelete("DeleteManufacture")]
        public async Task<IActionResult> DeleteManufacture([FromQuery] ApiGetById model)
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
                logger.LogError(ex, "{Repo} GetAllManufacture function error", JsonConvert.SerializeObject(model));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Добавление мануфактуры
        /// </summary>
        /// <param name="manufacture"> параметры мануфактуры </param>
        /// <returns>Результат</returns>
        [HttpPost("AddNewManufacture")]
        public async Task<IActionResult> AddNewManufacture(ManufactureModel manufacture)
        {
            try
            {
                var entity = mapper.Map<Manufacture>(manufacture);
                await repository.Add(entity);
                await repository.SaveAsync();

                return Ok(new { Id = entity.Id });//CreatedAtAction("GetItem", new { manufacture.Id }, manufacture);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllManufacture function error", JsonConvert.SerializeObject(manufacture));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Обновление мануфактуры
        /// </summary>
        /// <param name="manufacture"> параметры мануфактуры </param>
        /// <returns>Результат</returns>
        [HttpPut("UpdateManufacture")]
        public async Task<IActionResult> UpdateManufacture( Manufacture manufacture)
        {
            try
            {
                if (manufacture.Id != manufacture.Id) { return BadRequest(); }

                await repository.Update(manufacture);
                await repository.SaveAsync();

                // following up the REST standart on update, we need to return NoContent
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllManufacture function error", JsonConvert.SerializeObject(manufacture));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
