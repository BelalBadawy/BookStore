using System;
using System.Threading.Tasks;

namespace BS.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        RepositoryType Repository<RepositoryType>() where RepositoryType : class;
        Task<int> CommitAsync();
    }

}
