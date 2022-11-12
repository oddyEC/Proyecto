
using System.Linq.Expressions;

namespace Curso.ComercioElectronico.Domain;


public interface IRepository<TEntity,TEntityId> where TEntity : class
{
    
    IUnitOfWork UnitOfWork { get; }
    IQueryable<TEntity> GetAll(bool asNoTracking = true);
    //TODO: Utilizar genericos en el tipo del parametro id
    //Opción 2: Interfaz para GUID,int,string, quitar herencia de la clase base
    //
    Task<TEntity> GetByIdAsync(TEntityId id);

    Task<TEntity> AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    void Delete(TEntity entity);

    IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
}
