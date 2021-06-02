using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Domain.Models
{
    public class RegistrationModel
    {
        [Required]
   
        public string FullName { get; set; }

     
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        //[MinLength(6)]
        //public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
