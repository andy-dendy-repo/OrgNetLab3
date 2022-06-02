using OrgNetLab3.Data.Core;
using Microsoft.AspNetCore.Mvc;
using OrgNetLab3.Token;

namespace OrgNetLab3.Controllers
{
    public class BaseController<T> : ControllerBase
    {
        private readonly IBaseRepository<T> _baseRepository;
        public BaseController(IBaseRepository<T> baseRepository, IHttpContextAccessor httpContextAccessor)
        {
            _baseRepository = baseRepository;
            _baseRepository.UserId = httpContextAccessor.GetUserId();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _baseRepository.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _baseRepository.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(T model)
        {
            try
            {
                await _baseRepository.Create(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(T model)
        {
            try
            {
                await _baseRepository.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(params object[] keys)
        {
            try
            {
                await _baseRepository.Delete(keys);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
