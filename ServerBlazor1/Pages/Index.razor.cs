using MessageSynchroniser.Domain.ValueObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ServerBlazor1.Pages
{
	public partial class Index
	{
		private HubConnection? _connection;
		private Dictionary<string, string> _userMessageDictionary = new();
		private string myId = string.Empty;


		protected override async Task OnInitializedAsync()
		{
			_connection = new HubConnectionBuilder()
				.WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"))
				.Build();

			_connection.On<Notification>("ReceiveMessage", (notification) =>
			{
				_userMessageDictionary[notification.Source] = notification.Content;
				InvokeAsync(StateHasChanged);

			});

			_connection.On<List<string>>("GetClients", (message) =>
			{
				_userMessageDictionary = message.Where(m => !m.Equals(myId)).ToDictionary(m => m , m => string.Empty);
				InvokeAsync(StateHasChanged);
			});

			await _connection.StartAsync();
			myId = _connection.ConnectionId;
		}

		private async Task SendDirectMessage(string userId, string message)
		{
			await _connection.InvokeAsync("SendMessageToClient", userId, message);
		}

		public async ValueTask DisposeAsync()
		{
			if (_connection is not null)
			{
				await _connection.DisposeAsync();
			}
		}
	}
}