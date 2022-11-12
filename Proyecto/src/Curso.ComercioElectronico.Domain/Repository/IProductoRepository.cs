namespace Curso.ComercioElectronico.Domain
{
    public interface IProductoRepository : IRepository<Producto, int>
    {
        Task<bool> ExisteNombre(string nombre);

        Task<bool> ExisteNombre(string nombre, int idExcluir);
        Task<ICollection<Producto>> GetListAsync(IList<int> listaIds, bool asNoTracking = true);
    }
}