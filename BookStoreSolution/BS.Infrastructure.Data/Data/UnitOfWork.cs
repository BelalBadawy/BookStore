using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreStore.Infrastructure.Data;
using BS.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BS.Infrastructure.Data.Data
{
    public class UnitOfWork :  IUnitOfWork  , IDisposable
    {
        private Dictionary<(Type type, string name), object> _repositories;

        public UnitOfWork(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Initializes an instance of the repository
        /// </summary>
        /// <typeparam name="TEntity">The entity type to initialize with</typeparam>
        /// <returns>An initialized repository</returns>
        public RepositoryType Repository<RepositoryType>()
        {
            return (RepositoryType)GetOrAddRepository(typeof(RepositoryType), new Repository<RepositoryType>(Context));
        }

        public DbContext Context { get; }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Releases the allocated resources for this context
        /// </summary>
        public void Dispose()
        {
            Context?.Dispose();
        }

        internal object GetOrAddRepository(Type type, object repo)
        {
            // Initialize dictionary if it is null
            _repositories ??= new Dictionary<(Type type, string Name), object>();

            // Pull out the repository if it exists
            if (_repositories.TryGetValue((type, repo.GetType().FullName), out var repository)) return repository;

            // Add the repository to the dictionary
            _repositories.Add((type, repo.GetType().FullName), repo);
            return repo;
        }

     
    }
}
