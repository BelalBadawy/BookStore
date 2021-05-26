using System;
using BS.Domain.Common;

namespace BS.Application.Dtos
{
	public class BookCategoryUpsertDto : AuditableBaseEntity
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public int DisplayOrder { get; set; }
		public bool IsActive { get; set; }


	}
}
