using System.ComponentModel.DataAnnotations;
namespace DatingApp.API.Dtos
{
    public class UserForLoginDto
    {
         
        public string username{get; set;}
       
        public string Password{get; set;}
    }
}