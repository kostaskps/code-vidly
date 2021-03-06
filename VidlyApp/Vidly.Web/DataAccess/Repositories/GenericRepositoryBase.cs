using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vidly.Web.Contracts;

namespace Vidly.Web.DataAccess.Repositories
{
    public class GenericRepositoryBase<T> : IProvideGenericRepository<T> where T : class
    {
        private readonly VidlyDBContext _context;
        public GenericRepositoryBase(VidlyDBContext context)
        {
            _context = context;
        }

        protected VidlyDBContext VidlyDbContext { get { return _context; } }

        public void Add(T entity)
        {
            VidlyDbContext.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            VidlyDbContext.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> GetAll()
        {
            return VidlyDbContext.Set<T>().ToList();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await VidlyDbContext.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        public T GetById(int id)
        {
            return VidlyDbContext.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            VidlyDbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            VidlyDbContext.Set<T>().RemoveRange(entities);
        }
    }
}
