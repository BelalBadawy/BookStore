using System;
using System.ComponentModel.DataAnnotations;

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
