using System.ComponentModel.DataAnnotations;

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
