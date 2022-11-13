using Curso.ComercioElectronico.Application.Dtos;

namespace Curso.ComercioElectronico.Application
{
    public interface IClienteAppService
    {
        Task<ListaPaginada<ClienteDto>> GetAll(string buscar, int limit = 10, int offset = 0);

        Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteDto);

        Task UpdateAsync(Guid id, ClienteCrearActualizarDto clienteDto);

        Task<bool> DeleteAsync(Guid clienteId);
    }
}