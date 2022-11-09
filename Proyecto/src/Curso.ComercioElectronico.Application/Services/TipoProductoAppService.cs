using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application
{
    public class TipoProductoAppService : ITipoProductoService
    {

        private readonly ITipoProductoRepository repository;
        //private readonly IUnitOfWork unitOfWork;

        public TipoProductoAppService(ITipoProductoRepository repository)
        {
            this.repository = repository;
            //this.unitOfWork = unitOfWork;
        }
        public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tproducto)
        {
            //Reglas Validaciones... 
            var existeNombreMarca = await repository.ExisteNombre(tproducto.Nombre);
            if (existeNombreMarca)
            {
                throw new ArgumentException($"Ya existe una marca con el nombre {tproducto.Nombre}");
            }

            //Mapeo Dto => Entidad
            var tipoProducto = new TipoProducto();
            tipoProducto.Nombre = tproducto.Nombre;

            //Persistencia objeto
            tipoProducto = await repository.AddAsync(tipoProducto);
            //await unitOfWork.SaveChangesAsync();

            //Mapeo Entidad => Dto
            var tipoProductoCreada = new TipoProductoDto();
            tipoProductoCreada.Nombre = tipoProducto.Nombre;
            tipoProductoCreada.Id = tipoProducto.Id;

            //TODO: Enviar un correo electronica... 

            return tipoProductoCreada;
        }

        public async Task<bool> DeleteAsync(int tproductoId)
        {
            var tproducto = await repository.GetByIdAsync(tproductoId);
            if (tproducto == null)
            {
                throw new ArgumentException($"El tipo de producto con el id: {tproductoId}, no existe");
            }
            repository.Delete(tproducto);
            return true;
        }

        public ICollection<TipoProductoDto> GetAll()
        {
            var tipoProductoList = repository.GetAll();
            var tipoProductoListDto = from t in tipoProductoList
                                      select new TipoProductoDto()
                                      {
                                          Id = t.Id,
                                          Nombre = t.Nombre
                                      };
            return tipoProductoListDto.ToList();
        }

        public async Task UpdateAsync(int id, TipoProductoCrearActualizarDto tproducto)
        {
            var tipoProducto = await repository.GetByIdAsync(id);
            if (tipoProducto == null)
            {
                throw new ArgumentException($"El tipo de producto con el id: {id}, no existe");
            }
            var existeNombreTipoProducto = await repository.ExisteNombre(tproducto.Nombre, id);
            if (existeNombreTipoProducto)
            {
                throw new ArgumentException($"Ya existe una marca con el nombre {tproducto.Nombre}");
            }
            //mapeo Dto => Entidad
            tipoProducto.Nombre = tproducto.Nombre;
            await repository.UpdateAsync(tipoProducto);

            return;
        }
    }
}