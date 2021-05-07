using System.Collections.Generic;
using System.Linq;
using Vidly.Web.Contracts;
using Vidly.Web.Models;

namespace Vidly.Web.DataAccess.Repositories
{
    public class GenresRepository : GenericRepositoryBase<Genre>, IProvideGenres
    {
        public GenresRepository(VidlyDBContext context) : base(context)
        {
        }

        //public IEnumerable<Genres> GetAllGenres()
        //{
        //    return GetAll();
        //}
        
    }
}
