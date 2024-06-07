using MessageSynchroniser.Application.Contracts.Persistence;
using MessageSynchroniser.Application.Models;
using MessageSynchroniser.Domain.ValueObjects;
using Microsoft.AspNetCore.SignalR;

namespace ServerBlazor1.Hubs
{
	public class ChatHub : Hub
	{
		private static List<string> clients = new();
		private readonly INotificationRepository _notificationRepository;

		public override async Task<Task> OnConnectedAsync()
		{
			clients.Add(Context.ConnectionId);
			await PublicAvailableClients();

			return base.OnConnectedAsync();
		}
        public ChatHub(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
		}
        public async Task SendMessage(Notification notification)
		{
			NotificationDto notificationDto = new NotificationDto()
			{
				Content = notification.Content,
				Source = notification.Source,
				DateCreated = notification.DateCreated,
				Destination = notification.Destination,
			};
			await _notificationRepository.CreateAsync(notificationDto);
			await Clients.Others.SendAsync("ReceiveMessage", notification);
		}
		public async Task SendMessageToClient(string clientId, string message)
		{
			await Clients.Client(clientId).SendAsync("ReceiveMessageFromServer", "server", message);
		}
		public async Task PublicAvailableClients()
		{
			await Clients.All.SendAsync("GetClients", clients);
		}

		public override async Task<Task> OnDisconnectedAsync(Exception exception)
		{
			string userId = Context.ConnectionId;
			clients.Remove(userId);
			await PublicAvailableClients();

			return base.OnDisconnectedAsync(exception);
		}
	}
}
