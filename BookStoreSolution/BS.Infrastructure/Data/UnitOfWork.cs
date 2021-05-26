using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BS.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private Dictionary<Type, object> _repositories;
        public DbContext _context { get; }
        public UnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Initializes an instance of the repository
        /// </summary>
        /// <typeparam name="TEntity">The entity type to initialize with</typeparam>
        /// <returns>An initialized repository</returns>
        public RepositoryType Repository<RepositoryType>() where RepositoryType : class
        {
            // return (RepositoryType) GetOrAddRepository(typeof(RepositoryType), new RepositoryType(Context));

            var interfaceType = typeof(RepositoryType);


            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();


            }
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(x => x.GetInterface(interfaceType.Name) != null).FirstOrDefault();

            if (type != null)
            {
                if (!_repositories.ContainsKey(interfaceType))
                {
                    _repositories[interfaceType] = Activator.CreateInstance(type, _context);
                }

                return (RepositoryType)_repositories[interfaceType];

            }

            return null;
        }



        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Releases the allocated resources for this context
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }

        //internal object GetOrAddRepository(Type type, object repo)
        //{
        //    // Initialize dictionary if it is null
        //    _repositories ??= new Dictionary<(Type type, string Name), object>();

        //    // Pull out the repository if it exists
        //    if (_repositories.TryGetValue((type, repo.GetType().FullName), out var repository)) return repository;

        //    // Add the repository to the dictionary
        //    _repositories.Add((type, repo.GetType().FullName), repo);
        //    return repo;
        //}


    }
}
