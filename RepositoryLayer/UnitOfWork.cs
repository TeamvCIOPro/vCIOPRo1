using System;
using CoreEntities.Domain;
using System.Data.Entity.Infrastructure;

namespace RepositoryLayer
{
    /// <summary>
    /// Unit of work implementation for having single instance of context and doing DB operation as transaction
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>

    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private vCIOPRoEntities _context;

        public UnitOfWork(vCIOPRoEntities context)
        {
            _context = context;
        }

        public IGenericRepository<T> GetRepositoryInstance<T>() where T : class, new()
        {
            return new GenericRepository<T>(_context);
        }
        public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>(sql, parameters);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //#region Repositories

        //IGenericRepository<UserDetails> _userDetailsRepository;
        //public IGenericRepository<UserDetails> UserDetailsRepository
        //{
        //    get
        //    {
        //        if (_userDetailsRepository == null)
        //        {
        //            _userDetailsRepository = new GenericRepository<UserDetails>(_context);
        //        }

        //        return _userDetailsRepository;
        //    }
        //}



        //#endregion
    }
}
