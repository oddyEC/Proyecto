using Curso.ComercioElectronico.Application.Dtos;

namespace Curso.ComercioElectronico.Application.Interfaces
{
    public interface ITipoClienteAppService
    {
        Task<TipoClienteDto> GetByIdAsync(int id);

        ListaPaginada<TipoClienteDto> GetAll(int limit = 10, int offset = 0);

        Task<TipoClienteDto> CreateAsync(TipoClienteCrearActualizarDto tcliente);

        Task UpdateAsync(int id, TipoClienteCrearActualizarDto tcliente);

        Task<bool> DeleteAsync(int tclienteId);
    }
}