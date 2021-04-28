using System.Collections.Generic;
using Vidly.Web.Models;

namespace Vidly.Web.Contracts
{
    public interface IProvideGenresRepository : IProvideVidlyRepository<Genre>
    {
        //IEnumerable<Genres> GetAllGenres();
    }
}
