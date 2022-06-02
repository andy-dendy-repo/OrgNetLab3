using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrgNetLab3.Controllers;
using OrgNetLab3.Data.Entity;
using OrgNetLab3.Data.Services;

namespace OrgNetLab3.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : BaseController<Subject>
    {
        private readonly SubjectRepository _subjectRepository;

        public SubjectController(SubjectRepository subjectRepository, IHttpContextAccessor httpContextAccessor) : base(subjectRepository, httpContextAccessor)
        {
            _subjectRepository = subjectRepository;
        }
    }
}
