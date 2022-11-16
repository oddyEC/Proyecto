namespace Curso.ComercioElectronico.Application
{
    public interface ITipoProductoService
    {
        Task<TipoProductoDto> GetByIdAsync(int id);

        ListaPaginada<TipoProductoDto> GetAll(int limit = 10, int offset = 0);

        Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tproducto);

        Task UpdateAsync(int id, TipoProductoCrearActualizarDto tproducto);

        Task<bool> DeleteAsync(int tproductoId);
    }
}