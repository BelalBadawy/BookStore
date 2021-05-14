using System;
using System.ComponentModel.DataAnnotations;

namespace BS.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public virtual Guid Id { get; set; }

        public Guid? CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

      

        public Guid? LastModifiedBy { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime? LastModified { get; set; }
	}
}
