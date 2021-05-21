
using System.Collections.Generic;

namespace BS.Domain.Common
{
  public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int FilteredTotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}

