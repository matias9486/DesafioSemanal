using DesafioSemanal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSemanal.Interfaces
{
    public interface IPostRepository:IBaseRepository<Post>
    {
        Post GetPost(int id);
        List<Post> GetPosts();
    }
}
