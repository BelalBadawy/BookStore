using System;

namespace BS.Application.Dtos
{
	public class BookCategoryReadDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public DateTime Created { get; set; }
		public Guid LastModifiedBy { get; set; }
		public DateTime? LastModified { get; set; }
		public int DisplayOrder { get; set; }
		public bool IsActive { get; set; }

	}
}
