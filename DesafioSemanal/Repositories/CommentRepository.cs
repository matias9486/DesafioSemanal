using DesafioSemanal.Context;
using DesafioSemanal.Interfaces;
using DesafioSemanal.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSemanal.Repositories
{
    public class CommentRepository : BaseRepository<Comment, BlogContext>, ICommentRepository
    {
        public CommentRepository(BlogContext dbContext) : base(dbContext)
        {
        }

        public Comment GetComment(int id)
        {
            return Dbset.Include(x => x.User).Include(c => c.Post).FirstOrDefault(x => x.Id == id);
        }

        public List<Comment> GetComments()
        {
            return Dbset.Include(x => x.User).Include(c => c.Post).ToList();
        }
    }
}
