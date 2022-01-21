using DesafioSemanal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSemanal.Repositories
{
    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
            //estos valores seran la entidad y el contexto con el que estamos trabajando
            where TEntity : class
            where TContext : DbContext //restringimos los tipos
        //es DbContext para que sea general, lo mismo que class
    {
        private readonly TContext _dbContext;

        //dbset generico para las relaciones con otras clases
        private DbSet<TEntity> _dbSet;
        protected DbSet<TEntity> Dbset
        {
            //retorna el dbSet o si es nulo hace un set de tipo entidad. ??= si es nulo asignale esto
            get { return _dbSet ??= _dbContext.Set<TEntity>(); }
        }

        protected BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TEntity> GetAllEntities()
        {
            /* como trabajamos con un contexto generico no sabemos con que entidad estamos trabajando
              entonces creamos un dbSet(usando .Set<TEntity>) de esa Entidad para manejar los datos
            equivale a _Context.Users.ToList()
             */
            return _dbContext.Set<TEntity>().ToList();
        }

        //filtro generico por id
        public TEntity GetEntity(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dbContext.Attach(entity); //.Attach, revisa si la entidad tuvo cambios o no
            _dbContext.Entry(entity).State = EntityState.Modified; //cambio el estado a modificado

            //_dbContext.Update(entity); haria el attach y el entry
            _dbContext.SaveChanges(); //la guardamos
            return entity;
        }

        public void Delete(int id)
        {
            var entityDelete = _dbContext.Find<TEntity>(id);
            _dbContext.Remove(entityDelete);
            _dbContext.SaveChanges();
        }
    }
}
