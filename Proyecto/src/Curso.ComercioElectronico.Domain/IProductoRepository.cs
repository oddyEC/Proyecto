namespace Curso.ComercioElectronico.Domain
{
    public interface IProductoRepository : IRepository<Producto>
    {
        Task<bool> ExisteNombre(string nombre);

        Task<bool> ExisteNombre(string nombre, int idExcluir);
    }
}