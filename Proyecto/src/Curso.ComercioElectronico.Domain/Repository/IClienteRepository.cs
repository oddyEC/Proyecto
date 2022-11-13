namespace Curso.ComercioElectronico.Domain.Repository
{
    public interface IClienteRepository : IRepository<Cliente,Guid>
    {
        Task<bool> ExisteNombre(string nombre);
    }
}