using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
    
        [Required]
        public string username{get; set;}
         [Required]
         [StringLength(8,MinimumLength=3,ErrorMessage="You must specify password between 4 to 8 characters")]
        public string Password{get; set;}
    }
}