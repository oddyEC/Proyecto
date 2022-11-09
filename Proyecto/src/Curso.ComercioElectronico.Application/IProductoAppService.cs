namespace Curso.ComercioElectronico.Application
{
    public interface IProductoAppService
    {
        Task<ProductoDto> GetByIdAsync(int id);

        ListaPaginada<ProductoDto> GetAll(int limit = 10, int offset = 0);

        Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto productoDto);

        Task UpdateAsync(int id, ProductoCrearActualizarDto productoDto);

        Task<bool> DeleteAsync(int productoId);

        Task<ProductoDto> GetByName(string nombre);
    }

}