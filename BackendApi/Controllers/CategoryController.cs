using Microsoft.AspNetCore.Mvc;
using BackendHelper.Repositories.IRepos;
using BackendEntities.RequestModels;
using Newtonsoft.Json;
using BackendEntities.RequestModels.EntityModels;
using AutoMapper;
using BackendEntities.Entities;
using BackendHelper.Helpers;

namespace BackendEntities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public CategoryController(ICategoryRepo repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <returns>Результат</returns>
        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            try
            {
                var result = await repository.All();
                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllCategories function error", typeof(CategoryController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Получение категории
        /// </summary>
        /// <param name="id">идентификатор категории</param>
        /// <returns>Результат</returns>
        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                var result = await repository.GetById(id);

                if (result == null) { return NotFound(); }

                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "{Repo} GetCategoryById function error", typeof(CategoryController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="model">модель идентификатора категории</param>
        /// <returns>Результат</returns>
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromQuery] ApiGetById model)
        {
            try
            {
                var result = await repository.GetById(model.Id);

                if (result == null) { return NotFound($"Категория с id {model.Id} не найдена"); }

                await repository.Delete(model.Id);
                await repository.SaveAsync();

                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Ошибка удаление суда model = {model}", JsonConvert.SerializeObject(model));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Добавление категории
        /// </summary>
        /// <param name="category">параметры категории</param>
        /// <returns>Результат</returns>
        [HttpPost("AddNewCategory")]
        public async Task<IActionResult> AddNewCategory(CategoryModel category)
        {
            try
            {
                var entity = mapper.Map<CategoryModel,Category>(category);
                await repository.Add(entity);
                await repository.SaveAsync();

                return Ok(new { Id = entity.Id });
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "{Repo} AddNewCategory function error", JsonConvert.SerializeObject(category));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <param name="category">параметры категории</param>
        /// <returns>Результат</returns>
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            try
            {
                if (category == null) { return BadRequest(); }

                await repository.Update(category);
                await repository.SaveAsync();

                // following up the REST standart on update, we need to return NoContent
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} UdateCategory function error", JsonConvert.SerializeObject(category));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
