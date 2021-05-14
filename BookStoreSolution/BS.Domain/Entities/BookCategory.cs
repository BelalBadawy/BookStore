using System;
using System.ComponentModel.DataAnnotations;
using BS.Domain.Common;

namespace BS.Domain.Entities
{
	public class BookCategory: AuditableBaseEntity
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
		
		#endregion


	}
}
