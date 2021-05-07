using Vidly.Web.Contracts;
using Vidly.Web.Models;

namespace Vidly.Web.DataAccess.Repositories
{
    public class MembershipTypesRepository : GenericRepositoryBase<MembershipType>, IProvideMembershipTypes
    {
        public MembershipTypesRepository(VidlyDBContext context) : base(context)
        {

        }
    }
}
