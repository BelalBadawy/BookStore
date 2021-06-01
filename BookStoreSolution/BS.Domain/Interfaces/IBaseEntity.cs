using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Domain.Interfaces
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }

    }
}
