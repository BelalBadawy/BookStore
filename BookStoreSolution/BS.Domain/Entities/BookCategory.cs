using System;
using System.ComponentModel.DataAnnotations;
using BS.Domain.Common;
using BS.Domain.Interfaces;

namespace BS.Domain.Entities
{
    public class BookCategory : IBaseEntity, IAuditable, ISoftDelete, IDataConcurrency
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BookCategory class.
        /// </summary>
        public BookCategory()
        {

        }

        #endregion

        #region Properties

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

        #endregion


    }
}
