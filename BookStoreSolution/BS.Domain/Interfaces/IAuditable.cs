using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Domain.Interfaces
{
    public interface IAuditable
    {
        public Guid? CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public Guid? LastModifiedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedAt { get; set; }
    }
}
