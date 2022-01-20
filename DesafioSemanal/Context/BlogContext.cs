using Microsoft.EntityFrameworkCore;

using DesafioSemanal.Model;

namespace DesafioSemanal.Context
{
    public class BlogContext:DbContext
    {
        public BlogContext() 
        { }

        //https://www.entityframeworktutorial.net/efcore/entity-framework-core-console-application.aspx
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=HATAKE-PC\SQLEXPRESS01;Database=BlogDB;Trusted_Connection=True;");
        }

        //Entidades
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
