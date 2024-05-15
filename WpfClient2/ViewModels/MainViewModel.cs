using WpfClient2.Services;

namespace WpfClient2.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private string _sendMessageContent = string.Empty;
		private string _receivedMessageContent = string.Empty;
		private readonly ISignalRService _signalRService;

		public string SendMessageContent
		{
			get
			{
				return _sendMessageContent;
			}
			set
			{
				_sendMessageContent = value;
				OnPropertyChanged(nameof(SendMessageContent));
				_signalRService.SendMessage("", _sendMessageContent);
			}
		}

		public string ReceivedMessageContent
		{
			get
			{
				return _receivedMessageContent;
			}
			set
			{
				_receivedMessageContent = value;
				OnPropertyChanged(nameof(ReceivedMessageContent));
			}
		}



		public MainViewModel(ISignalRService signalRService)
		{
			_signalRService = signalRService;
			_signalRService.ConnectAsync();
			_signalRService.DirectMessage += NewDirectMessage;

		}

		private void NewDirectMessage(string name, string message)
		{
			ReceivedMessageContent = message;
		}
	}
}
