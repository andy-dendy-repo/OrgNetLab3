using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrgNetLab3.Controllers;
using OrgNetLab3.Data.Entity;
using OrgNetLab3.Data.Services;

namespace OrgNetLab3.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGroupController : BaseController<StudentGroup>
    {
        private readonly StudentGroupRepository _studentGroupRepository;

        public StudentGroupController(StudentGroupRepository studentGroupRepository, IHttpContextAccessor httpContextAccessor) : base(studentGroupRepository, httpContextAccessor)
        {
            _studentGroupRepository = studentGroupRepository;
        }

        [HttpGet("{groupId}/{studentId}")]
        public async Task<IActionResult> GetById(Guid groupId, Guid studentId)
        {
            try
            {
                return Ok(await _studentGroupRepository.GetById(groupId, studentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
