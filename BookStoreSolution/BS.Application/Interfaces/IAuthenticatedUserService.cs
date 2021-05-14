using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        Guid? UserId { get; }
    }
}
