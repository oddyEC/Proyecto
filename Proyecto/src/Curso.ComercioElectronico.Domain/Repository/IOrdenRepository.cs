namespace Curso.ComercioElectronico.Domain.Repository
{
    public interface IOrdenRepository : IRepository<Orden>
    {
         Task<Orden> GetByIdAsync(Guid id);
    }
}