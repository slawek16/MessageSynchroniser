using MessageSynchroniser.Application.Contracts.Persistence;
using MessageSynchroniser.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace MessageSynchroniser.Persistence.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly MessageSynchroniserDatabaseContetx _context;
		public GenericRepository(MessageSynchroniserDatabaseContetx context)
		{
			_context = context;
		}

		public async Task CreateAsync(T entity)
		{
			await _context.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _context.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T> GetAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}
	}
}
