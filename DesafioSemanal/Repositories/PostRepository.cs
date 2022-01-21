using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioSemanal.Context;
using DesafioSemanal.Model;
using DesafioSemanal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioSemanal.Repositories
{
    public class PostRepository : BaseRepository<Post, BlogContext>, IPostRepository
    {
        public PostRepository(BlogContext dbContext) : base(dbContext)
        {
        }

        public Post GetPost(int id)
        {
            return Dbset.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public List<Post> GetPosts()
        {
            return Dbset.Include(x => x.User).ToList();
        }
    }
}
