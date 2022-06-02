using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrgNetLab3.Controllers;
using OrgNetLab3.Data.Entity;
using OrgNetLab3.Data.Services;
using OrgNetLab3.Token;

namespace OrgNetLab3.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        private readonly UserRepository _userRepository;
        public UserController(UserRepository userRepository, IHttpContextAccessor httpContextAccessor) : base(userRepository, httpContextAccessor)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await _userRepository.Register(user);

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("hashPassword")]
        public async Task<IActionResult> UpdateAndHashPassword(User user)
        {
            try
            {
                await _userRepository.UpdateAndHashPassword(user);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/students/lesson/{lessonId}")]
        public async Task<IActionResult> GetStudentsByLesson(Guid lessonId)
        {
            return Ok(await _userRepository.GetStudentsByLesson(lessonId));
        }

        [HttpGet("student/schedule")]
        public async Task<IActionResult> GetSchedule([FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var id = httpContextAccessor.GetUserId();

            var schedule = await _userRepository.GetSchedule(Guid.Parse(id));

            return Ok(schedule);
        }
    }
}
