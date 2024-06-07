using MessageSynchroniser.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageSynchroniser.Persistence.DatabaseContext
{
	public class MessageSynchroniserDatabaseContetx : DbContext
	{
		public MessageSynchroniserDatabaseContetx(DbContextOptions<MessageSynchroniserDatabaseContetx> options) : base(options) { }
		public DbSet<NotificationDto> Notifications { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessageSynchroniserDatabaseContetx).Assembly);

			base.OnModelCreating(modelBuilder);
		}
	}
}
