using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Domain.Interfaces
{
    public interface IDataConcurrency
    {
        public byte[] RowVersion { get; set; }
    }
}
