using Microsoft.AspNetCore.SignalR;

namespace ServerBlazor1.Hubs
{
	public class ChatHub :Hub
	{
		private static List<string> clients = new();
		public override Task OnConnectedAsync()
		{
			clients.Add(Context.ConnectionId);
			PublicAvailableClients();

			return base.OnConnectedAsync();
		}
		public async Task SendMessage(string user, string message)
		{
			await Clients.Others.SendAsync("ReceiveMessage", user, message);
		}
		public async Task SendMessageToClient(string clientId, string message)
		{
			await Clients.Client(clientId).SendAsync("ReceiveMessageFromServer", "server", message);
		}
		public async Task PublicAvailableClients()
		{
			await Clients.All.SendAsync("GetClients", clients);
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			string userId = Context.ConnectionId;
			clients.Remove(userId);
			PublicAvailableClients();

			return base.OnDisconnectedAsync(exception);
		}
	}
}
