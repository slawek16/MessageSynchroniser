namespace WpfClient2.Services
{
	public interface ISignalRService
    {
		event Action<string, string> DirectMessage;
		Task ConnectAsync();
		Task SendMessage(string user, string message);
	}
}
