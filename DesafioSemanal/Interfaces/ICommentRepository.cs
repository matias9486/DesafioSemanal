using DesafioSemanal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSemanal.Interfaces
{
    public interface ICommentRepository: IBaseRepository<Comment>
    {
        Comment GetComment(int id);
        List<Comment> GetComments();
    }
}
