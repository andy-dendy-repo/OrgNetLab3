using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrgNetLab3.Controllers;
using OrgNetLab3.Data.Entity;
using OrgNetLab3.Data.Services;

namespace OrgNetLab3.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : BaseController<Message>
    {
        private readonly MessageRepository _messageRepository;

        public MessageController(MessageRepository messageRepository, IHttpContextAccessor httpContextAccessor) : base(messageRepository, httpContextAccessor)
        {
            _messageRepository = messageRepository;
        }
    }
}
