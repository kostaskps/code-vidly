using System;

namespace Vidly.Web.Contracts
{
    public interface IProvideUnitOfWork : IDisposable
    {
        IProvideGenres Genres { get; }
        int Complete();
    }
}
