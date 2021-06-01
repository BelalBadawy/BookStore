using System;
using BS.Domain.Common;
using BS.Domain.Interfaces;

namespace BS.Application.Dtos
{
    public class BookCategoryReadDto : IBaseEntity, IAuditable, ISoftDelete, IDataConcurrency

    {
     
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public bool SoftDeleted { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid Id { get; set; }

    }
}
