using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.SignalR;
using OrgNetLab3.Data.Entity;
using OrgNetLab3.Data.Services;
using System.Collections.Concurrent;

namespace OrgNetLab3
{
    public interface IChatClient
    {
        public Task NewMessageList(IList<Message> messages);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly MessageRepository _messageRepository;

        public ChatHub(MessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        private static ConcurrentDictionary<string, Guid> _users = new ConcurrentDictionary<string, Guid>();

        public override async Task OnConnectedAsync()
        {
            try
            {
                var httpContext = Context.Features.Get<IHttpContextFeature>().HttpContext;
                string id = httpContext.Request.Query["uid"];

                while (!_users.TryAdd(Context.ConnectionId, Guid.Parse(id))) ;

                await base.OnConnectedAsync();
            }
            catch
            {
            }
        }

        public async Task SendMessage(string to, string text)
        {
            Guid toId = Guid.Parse(to);

            Guid from = _users[Context.ConnectionId];

            Message message = new Message
            {
                Date = DateTime.Now,
                From = from,
                To = toId,
                Text = text,
                Id = Guid.NewGuid()
            };

            await _messageRepository.Create(message);

            var list = await GetMessages();

            string? connTo = _users.Where(x => x.Value == toId).Select(x => x.Key).FirstOrDefault();

            await Clients.Caller.NewMessageList(list);

            if(connTo != null)
                await Clients.Client(connTo).NewMessageList(list);
        }

        public async Task<IList<Message>> GetMessages()
        {
            var messages = await _messageRepository.Get();

            Guid id = _users[Context.ConnectionId];

            return messages.Where(x=>x.From == id || x.To == id).OrderBy(x=>x.Date).ToList();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _users.Remove(Context.ConnectionId, out _);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
