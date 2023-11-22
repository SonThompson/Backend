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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public CustomerController(ICustomerRepo repository, ILogger logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех заказчиков
        /// </summary>
        /// <returns>Результат</returns>
        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            try
            {
                var result = await repository.All();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllCustomers function error", typeof(CustomerController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Получение заказчика
        /// </summary>
        /// <param name="id">идентификатор заказчика</param>
        /// <returns>Результат</returns>
        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            try
            {
                var result = await repository.GetById(id);

                if (result == null) { return NotFound(); }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllCustomers function error", typeof(CustomerController));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Удаление заказчика
        /// </summary>
        /// <param name="model">модель идентификатора заказчика</param>
        /// <returns>Результат</returns>
        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer([FromQuery] ApiGetById model)
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
                logger.LogError(ex, "{Repo} GetAllCustomers function error", JsonConvert.SerializeObject(model));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Добавление заказчика
        /// </summary>
        /// <param name="customer">параметры заказчика</param>
        /// <returns>Результат</returns>
        [HttpPost("AddNewCustomer")]
        public async Task<IActionResult> AddNewCustomer(CustomerModel customer)
        {
            try
            {
                var entity = mapper.Map<Customer>(customer);
                await repository.Add(entity);
                await repository.SaveAsync();

                return Ok(new { Id = entity.Id });//CreatedAtAction("GetItem", new { entity.Id }, entity);
            }
           catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllCustomers function error", JsonConvert.SerializeObject(customer));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Обновление заказчика
        /// </summary>
        /// <param name="customer">параметры заказчика</param>
        /// <returns>Результат</returns>
        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer( Customer customer)
        {
            try
            {
                if (customer.Id != customer.Id) { return BadRequest(); }

                await repository.Update(customer);
                await repository.SaveAsync();

                // following up the REST standart on update, we need to return NoContent
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Repo} GetAllCustomers function error", JsonConvert.SerializeObject(customer));
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}
