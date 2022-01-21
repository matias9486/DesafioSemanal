using DesafioSemanal.Model;
using System.Collections.Generic;

namespace DesafioSemanal.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        /*tendra los metodos bases, comunes a todos*/

        //métodos propios que tendrá la clase
        User GetUser(int id);
        List<User> GetUsers();
    }
}