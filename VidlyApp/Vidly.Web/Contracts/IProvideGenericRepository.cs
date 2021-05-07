using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vidly.Web.Contracts
{
    /// <summary>
    /// This is the base Generic Interface for Repositories that defines the basic Entity operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProvideGenericRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        Task<IList<T>> GetAllAsync();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
