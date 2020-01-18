namespace DatingApp.API.Models
{
    public class User
    {
        public int id{get; set;}

        public string username{get; set;}

        public byte[] Password{get; set;}

        public byte[] PassworSalt{get;set;}
    }
}