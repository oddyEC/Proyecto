using AutoMapper;
using Curso.ComercioElectronico.Application.Dtos;
using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

/**
* TODO: Implementar todos los metodos del servicio de aplicacion de clientes
*/
public class ClienteAppService : IClienteAppService
{

    private readonly IClienteRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<TipoProductoAppService> logger;

    public ClienteAppService(IClienteRepository repository, IMapper mapper, ILogger<TipoProductoAppService> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteDto)
    {
        //TODO: Aplicar validaciones
        logger.LogInformation("Crear Cliente");

        //Mapeo Dto => Entidad
        // var cliente = new Cliente();
        // cliente.Id = Guid.NewGuid();
        // cliente.Nombres = clienteDto.Nombres;
        var cliente = mapper.Map<Cliente>(clienteDto);
        //Persistencia objeto
        cliente = await repository.AddAsync(cliente);
        await repository.UnitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        // var clienteCreado = new ClienteDto();
        // clienteCreado.Id = cliente.Id;
        // clienteCreado.Nombres = cliente.Nombres;
        var clienteCreado = mapper.Map<ClienteDto>(cliente);
        return clienteCreado;
    }

    public async Task<bool> DeleteAsync(Guid clienteId)
    {
        var cliente = await repository.GetByIdAsync(clienteId);
        if (cliente == null)
        {
            throw new ArgumentException($"El cliente con el id: {clienteId}, no existe");

        }
        repository.Delete(cliente);
        return true;

    }

    public async Task<ListaPaginada<ClienteDto>> GetAll(string buscar, int limit = 10, int offset = 0)
    {
        var clienteList = repository.GetAll();
        clienteList = clienteList.Where(x => x.Nombres == buscar);
        var existeNombreCliente = await repository.ExisteNombre(buscar);
        if (!existeNombreCliente)
        {
            throw new ArgumentException($"No existe un cliente con el nombre de {buscar}");
        }
        var total = clienteList.Count();
        var clienteListDto = clienteList.Skip(offset)
                                        .Take(limit)
                                        .Select(
                                            x => new ClienteDto
                                            {
                                                Id = x.Id,
                                                Nombres = x.Nombres
                                            }
                                        );
        var resultado = new ListaPaginada<ClienteDto>();
        resultado.Total = total;
        resultado.Lista = clienteListDto.ToList();

        return resultado;

    }

    public async Task UpdateAsync(Guid id, ClienteCrearActualizarDto clienteDto)
    {
        var cliente = await repository.GetByIdAsync(id);
        if (cliente == null)
        {
            throw new ArgumentException($"El cliente con el id: {id}, no existe");
        }
        cliente.Nombres = clienteDto.Nombres;
        await repository.UpdateAsync(cliente);

        return;
    }
}
