using System.Collections.Generic;
using Vidly.Web.Models;

namespace Vidly.Web.Contracts
{
    public interface IProvideGenres : IProvideGenericRepository<Genre>
    {
        //IEnumerable<Genres> GetAllGenres();
    }
}
