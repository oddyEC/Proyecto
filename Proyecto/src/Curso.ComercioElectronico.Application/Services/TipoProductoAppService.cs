using AutoMapper;
using Curso.ComercioElectronico.Domain;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application
{
    public class TipoProductoAppService : ITipoProductoService
    {
        private readonly ITipoProductoRepository tipoProductoRepository;
        private readonly IMapper mapper;
        private readonly ILogger<TipoProductoAppService> logger;
        public TipoProductoAppService(ITipoProductoRepository tipoProductoRepository,
            IMapper mapper,
            ILogger<TipoProductoAppService> logger)
        {
            this.tipoProductoRepository = tipoProductoRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tproducto)
        {
            logger.LogInformation("Crear Tipo Producto");
            var tipoProducto = mapper.Map<TipoProducto>(tproducto);
            tipoProducto = await tipoProductoRepository.AddAsync(tipoProducto);
            await tipoProductoRepository.UnitOfWork.SaveChangesAsync();
            var tipoProductoCreada = mapper.Map<TipoProductoDto>(tipoProducto);
            await tipoProductoRepository.UnitOfWork.SaveChangesAsync();
            return tipoProductoCreada;
        }

        public async Task<bool> DeleteAsync(int tproductoId)
        {
            var tproducto = await tipoProductoRepository.GetByIdAsync(tproductoId);
            if (tproducto == null)
            {
                throw new ArgumentException($"El tipo de producto con el id: {tproductoId}, no existe");
            }
            tipoProductoRepository.Delete(tproducto);
            await tipoProductoRepository.UnitOfWork.SaveChangesAsync();
            return true;
        }

        public ICollection<TipoProductoDto> GetAll()
        {
            var tipoProductoList = tipoProductoRepository.GetAll();
            var tipoProductoListDto = from t in tipoProductoList
                                      select new TipoProductoDto()
                                      {
                                          Id = t.Id,
                                          Nombre = t.Nombre
                                      };
            return tipoProductoListDto.ToList();
        }

        public ListaPaginada<TipoProductoDto> GetAll(int limit = 10, int offset = 0)
        {
            var consulta = tipoProductoRepository.GetAll();
            var total = consulta.Count();
            var listaTipoProductosDto = consulta.Skip(offset)
                                                .Take(limit)
                                                .Select(
                                                x => new TipoProductoDto()
                                                {
                                                    Id = x.Id,
                                                    Nombre = x.Nombre
                                                }
                                                );
            var resultado = new ListaPaginada<TipoProductoDto>();
            resultado.Total = total;
            resultado.Lista = listaTipoProductosDto.ToList();
            return resultado;
        }
        public Task<TipoProductoDto> GetByIdAsync(int id)
        {
            var consulta = tipoProductoRepository.GetAll();
            consulta = consulta.Where(x => x.Id == id);
            var consultaTipoProductoDto = consulta
                                          .Select(
                                            x => new TipoProductoDto()
                                            {
                                                Id = x.Id,
                                                Nombre = x.Nombre
                                            }
                                          );
            return Task.FromResult(consultaTipoProductoDto.SingleOrDefault());
        }

        public async Task UpdateAsync(int id, TipoProductoCrearActualizarDto tproducto)
        {
            var tipoProducto = await tipoProductoRepository.GetByIdAsync(id);
            if (tipoProducto == null)
            {
                throw new ArgumentException($"El tipo de producto con el id: {id}, no existe");
            }
            var existeNombreTipoProducto = await tipoProductoRepository.ExisteNombre(tproducto.Nombre, id);
            if (existeNombreTipoProducto)
            {
                throw new ArgumentException($"Ya existe una marca con el nombre {tproducto.Nombre}");
            }
            //mapeo Dto => Entidad
            tipoProducto.Nombre = tproducto.Nombre;
            await tipoProductoRepository.UpdateAsync(tipoProducto);
             await tipoProductoRepository.UnitOfWork.SaveChangesAsync();
            return;
        }
    }
}