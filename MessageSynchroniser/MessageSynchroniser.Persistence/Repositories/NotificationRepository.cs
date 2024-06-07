using MessageSynchroniser.Application.Contracts.Persistence;
using MessageSynchroniser.Application.Models;
using MessageSynchroniser.Persistence.DatabaseContext;

namespace MessageSynchroniser.Persistence.Repositories
{
	public class NotificationRepository : GenericRepository<NotificationDto>, INotificationRepository
	{
		public NotificationRepository(MessageSynchroniserDatabaseContetx context) : base(context)
		{
		}
	}
}
