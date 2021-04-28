using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Vidly.Web.Contracts
{
    /// <summary>
    /// This is a generic Interface that can be used by Vidly Domain Classes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProvideVidlyRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
