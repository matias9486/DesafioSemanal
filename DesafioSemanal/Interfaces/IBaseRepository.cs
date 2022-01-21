using System.Collections.Generic;

namespace DesafioSemanal.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Delete(int id);
        List<TEntity> GetAllEntities();
        TEntity GetEntity(int id);
        TEntity Update(TEntity entity);
    }
}