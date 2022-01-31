using Microsoft.EntityFrameworkCore;

using DesafioSemanal.Model;

namespace DesafioSemanal.Context
{
    public class BlogContext:DbContext
    {
        //public BlogContext(DbContextOptions options): base(options)
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        { }
        
        //Entidades
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
