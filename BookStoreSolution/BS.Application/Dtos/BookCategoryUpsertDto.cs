using System;
using BS.Domain.Common;
using BS.Domain.Interfaces;

namespace BS.Application.Dtos
{
	public class BookCategoryUpsertDto 
	{
		public string Title { get; set; }
		public int DisplayOrder { get; set; }
		public bool IsActive { get; set; }
        public Guid Id { get; set; }
	}
}
