using Microsoft.AspNetCore.SignalR.Client;
using System.Net;

namespace WpfClient2.Services
{
	public class SignalRService : ISignalRService
	{
		public event Action<string, string> DirectMessage;

		private HubConnection? _connection;
		private string url = "http://localhost:5000/chatHub";
		public async Task ConnectAsync()
		{
			_connection = new HubConnectionBuilder()
				.WithUrl(url)
				.WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.FromSeconds(10) })
				.Build();

			_connection.On<string, string>("ReceiveMessageFromServer", (user, message) => DirectMessage?.Invoke(user, message));

			await _connection.StartAsync();

		}

		public async Task SendMessage(string user, string message)
		{
			await _connection.InvokeAsync("SendMessage", _connection.ConnectionId, message);
		}
	}
}
