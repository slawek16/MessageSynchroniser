namespace MessageSynchroniser.Domain.ValueObjects
{
	public class Notification
	{
		public int Id { get; set; }
		public string Source { get; }
		public string Destination { get; }
		public string Content { get; }
		public DateTime DateCreated { get; }
		public Notification(string source, string destination, string content, DateTime dateCreated)
		{
			Source = source;
			Destination = destination;
			Content = content;
			DateCreated = dateCreated;
		}
	}
}
