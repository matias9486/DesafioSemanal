using DesafioSemanal.Context;
using DesafioSemanal.Model;
using DesafioSemanal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace DesafioSemanal.Repositories
{
    public class UserRepository : BaseRepository<User, BlogContext> , IUserRepository
    {
        public UserRepository(BlogContext dbContext) : base(dbContext)
        {
        }

        //implementacion de metodo propio de la clase
        public User GetUser(int id)
        {
            //Include, requiere using Microsoft.EntityFramework
            //agregar using System.Linq para usar linq
            return Dbset.Include(x => x.Posts).Include(c=>c.Comments).FirstOrDefault(x=>x.Id==id);
            //return GetEntity(id);
        }
        public List<User> GetUsers()
        {
            return Dbset.Include(x => x.Posts).Include(c=>c.Comments).ToList();
        }

    }
}
