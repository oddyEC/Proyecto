using AutoMapper;
using Curso.ComercioElectronico.Application.Dtos;
using Curso.ComercioElectronico.Application.Interfaces;
using Curso.ComercioElectronico.Domain.Entidades;
using Curso.ComercioElectronico.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application.Services
{
    public class TipoClienteAppService : ITipoClienteAppService
    {
        private readonly ITipoClienteRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<TipoClienteAppService> logger;

        public TipoClienteAppService(ITipoClienteRepository repository, IMapper mapper, ILogger<TipoClienteAppService> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<TipoClienteDto> CreateAsync(TipoClienteCrearActualizarDto tcliente)
        {
            logger.LogInformation("Crear Tipo Producto");
            var tipoCliente = mapper.Map<TipoCliente>(tcliente);
            tipoCliente = await repository.AddAsync(tipoCliente);
            await repository.UnitOfWork.SaveChangesAsync();
            var tipoClienteCreado = mapper.Map<TipoClienteDto>(tipoCliente);
            await repository.UnitOfWork.SaveChangesAsync();
            return tipoClienteCreado;
        }

        public async Task<bool> DeleteAsync(string tclienteId)
        {
            var tproducto = await repository.GetByIdAsync(tclienteId);
            if (tproducto == null)
            {
                throw new ArgumentException($"El tipo de producto con el id: {tclienteId}, no existe");
            }
            repository.Delete(tproducto);
            await repository.UnitOfWork.SaveChangesAsync();
            return true;
        }

        public ListaPaginada<TipoClienteDto> GetAll(int limit = 10, int offset = 0)
        {
            var consulta = repository.GetAll();
            var total = consulta.Count();
            var listaTipoClientesDto = consulta.Skip(offset)
                                                .Take(limit)
                                                .Select(
                                                x => new TipoClienteDto()
                                                {
                                                    Id = x.Id,
                                                    Usuario = x.Usuario,
                                                    Password = x.Password,
                                                    TipoClienteEstado = (Dtos.TipoClienteEstado)x.TipoClienteEstado
                                                }
                                                );
            var resultado = new ListaPaginada<TipoClienteDto>();
            resultado.Total = total;
            resultado.Lista = listaTipoClientesDto.ToList();
            return resultado;
        }

        public Task<TipoClienteDto> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(string id, TipoClienteCrearActualizarDto tcliente)
        {
            var tipoCliente = await repository.GetByIdAsync(id);
            if(tipoCliente == null){
                throw new ArgumentException($"El tipo de cliente con el id: {id}, no existe");

            }
            tipoCliente.Usuario = tcliente.Usuario;
            tipoCliente.Password = tcliente.Password;
            await repository.UpdateAsync(tipoCliente);
            await repository.UnitOfWork.SaveChangesAsync();
        }
    }
}