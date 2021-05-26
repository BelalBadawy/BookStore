using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Domain.Models
{
    public class LogUserActivity
    {
		public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UrlData { get; set; }
        public string UserData { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string HttpMethod { get; set; }
	}
}
