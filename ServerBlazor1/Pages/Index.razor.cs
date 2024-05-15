using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using ServerBlazor1.Models;

namespace ServerBlazor1.Pages
{
	public partial class Index
	{
		private HubConnection? _connection;
		private List<UserMessage> _userMessages = new();
		private string myId = string.Empty;


		protected override async Task OnInitializedAsync()
		{
			_connection = new HubConnectionBuilder()
				.WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"))
				.Build();

			_connection.On<string, string>("ReceiveMessage", (user, message) =>
			{
				_userMessages.FirstOrDefault(u => u.UserName.Equals(user)).Message = message;
				InvokeAsync(StateHasChanged);
			});

			_connection.On<List<string>>("GetClients", (message) =>
			{
				_userMessages = message.Where(m => !m.Equals(myId)).Select(m => new UserMessage(){ UserName = m, Message = string.Empty }).ToList();
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