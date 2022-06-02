using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrgNetLab3.Controllers;
using OrgNetLab3.Data.Entity;
using OrgNetLab3.Data.Services;

namespace OrgNetLab3.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : BaseController<Group>
    {
        private readonly GroupRepository _groupRepository;

        public GroupController(GroupRepository groupRepository, IHttpContextAccessor httpContextAccessor) : base(groupRepository, httpContextAccessor)
        {
            _groupRepository = groupRepository;
        }
    }
}
