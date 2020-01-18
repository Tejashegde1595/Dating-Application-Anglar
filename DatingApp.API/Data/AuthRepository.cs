using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
namespace DatingApp.API.Data
{
    public class AuthRepository:IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context){
                _context=context;
        }
        

         public async Task<User> Register(User user,string password){
                byte[] passwordhash,passwordsalt;
                CreatePasswordHash(password,out passwordhash,out passwordsalt);

                user.Password=passwordhash;
                user.PassworSalt=passwordsalt;
                await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
         }

         private void CreatePasswordHash(string password,out byte[] passwordhash,out byte[] passwordsalt)
         {
             using(var hmac=new System.Security.Cryptography.HMACSHA512())
             {
                 passwordsalt=hmac.Key;
                 passwordhash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
             }
         }

        public async Task<User> Login(string username,string password){
                var user=await _context.users.FirstOrDefaultAsync(x=>x.username==username);

                if(user==null)
                return null;
               if(!VerifyPasswordHash(password,user.Password,user.PassworSalt))
               return null;

               return user;
        }

         private bool VerifyPasswordHash(string password,byte[] passwordhash,byte[] passwordsalt)
         {
             using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordsalt))
             {
                var computedhash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;i<computedhash.Length;i++)
                {
                    if(computedhash[i]!=passwordhash[i])
                    {
                        return false;
                    }
                }
             }
             return true;
         }


         public async Task<bool> UserExists(string username){
                if(await _context.users.AnyAsync(x=>x.username==username))
                    return true;
                
            return false;
         }
    }
}