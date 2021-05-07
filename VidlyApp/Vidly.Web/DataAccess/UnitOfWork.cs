using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Web.Contracts;

namespace Vidly.Web.DataAccess
{
    public class UnitOfWork : IProvideUnitOfWork
    {
        private bool disposedValue;
        private readonly VidlyDBContext _context;

        public UnitOfWork(VidlyDBContext context)
        {
            _context = context;
            Genres = new Repositories.GenresRepository(_context);
        }

        public IProvideGenres Genres { get; private set; }

        /// <summary>
        /// Save all changes to the Vidly Database
        /// </summary>
        /// <returns>the number of state entries written to the database</returns>
        public int Complete()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
