
namespace MessageSynchroniser.Application.Models
{
	public class NotificationDto
	{
		public int Id { get; set; }
		public string Source { get; set; }
		public string Destination { get; set; }
		public string Content { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
