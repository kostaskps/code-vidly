using System;

namespace Vidly.Web.Contracts
{
    public interface IProvideUnitOfWork : IDisposable
    {
        IProvideGenresRepository Genres { get; }
        int Complete();
    }
}
