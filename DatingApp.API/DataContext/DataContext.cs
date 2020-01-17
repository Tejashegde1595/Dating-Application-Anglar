using Microsoft.EntityFrameworkCore;
using DatingApp.API.Models;
namespace DatingApp.API.DataContext
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}

        public DbSet<Value> values{get;set;}
    }
}