
using Microsoft.EntityFrameworkCore;

namespace dotnetcore_api.Models 
{
    public class DatabaseContext : DbContext 
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlite("Filename=./test_db.sqlite");
        }
    }
}