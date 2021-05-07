using System;

namespace Vidly.Web.Contracts
{
    public interface IProvideUnitOfWork : IDisposable
    {
        IProvideGenres Genres { get; }

        IProvideMembershipTypes MembershipTypes { get; }

        int Complete();
    }
}
