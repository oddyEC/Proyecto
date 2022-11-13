using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Application.Dtos;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application
{
    public class ProductoAppService : IProductoAppService
    {
        private readonly IProductoRepository repository;
        private readonly IMarcaRepository marcaRepository;
        private readonly ITipoProductoRepository tipoProductoRepository;
        private readonly IMapper mapper;
        private readonly ILogger<TipoProductoAppService> logger;

        public ProductoAppService(IProductoRepository repository, IMarcaRepository marcaRepository, ITipoProductoRepository tipoProductoRepository, IMapper mapper, ILogger<TipoProductoAppService> logger)
        {
            this.repository = repository;
            this.marcaRepository = marcaRepository;
            this.tipoProductoRepository = tipoProductoRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto productoDto)
        {
            logger.LogInformation("Crear Producto");
            //Reglas Validaciones... 
            var existeNombreProducto = await repository.ExisteNombre(productoDto.Nombre);
            if (existeNombreProducto)
            {
                throw new ArgumentException($"Ya existe una marca con el nombre {productoDto.Nombre}");
            }

            //Mapeo Dto => Entidad
            //Mapeo Dto => Entidad
            var producto = mapper.Map<Producto>(productoDto);

            producto = await repository.AddAsync(producto);
            await repository.UnitOfWork.SaveChangesAsync();
            // producto.Caducidad = productoDto.Caducidad;
            // producto.MarcaId = productoDto.MarcaId;
            // producto.Nombre = productoDto.Nombre;
            // producto.Observaciones = productoDto.Observaciones;
            // producto.Precio = productoDto.Precio;
            // producto.TipoProductoId = productoDto.TipoProductoId;

            //Persistencia objeto
            var productoCreado = mapper.Map<ProductoDto>(producto);
            await repository.UnitOfWork.SaveChangesAsync();
            return productoCreado;
        }

        public async Task<bool> DeleteAsync(int productoId)
        {
            //Reglas Validaciones... 
            var producto = await repository.GetByIdAsync(productoId);
            if (producto == null)
            {
                throw new ArgumentException($"La marca con el id: {productoId}, no existe");
            }

            repository.Delete(producto);
            await repository.UnitOfWork.SaveChangesAsync();

            return true;
        }

        public ListaPaginada<ProductoDto> GetAll(int limit = 10, int offset = 0)
        {
            var consulta = repository.GetAllIncluding(x => x.Marca,
                                                        x => x.TipoProducto);

            //Lista 
            var consultaListaProductosDto = repository.GetAllIncluding(x => x.Marca,
            x => x.TipoProducto);
            // 1. Ejecvutar linq Total registros
            var total = consulta.Count();
            var listaProductosDto = consulta.Skip(offset)
                                            .Take(limit)

                                            .Select(
                                             x => new ProductoDto()
                                             {
                                                 Id = x.Id,
                                                 Caducidad = x.Caducidad,
                                                 //Utilizar propiedad navegacion,
                                                 //para obtener información de una clase relacionada
                                                 Marca = x.Marca.Nombre,
                                                 MarcaId = x.MarcaId,
                                                 Nombre = x.Nombre,
                                                 Observaciones = x.Observaciones,
                                                 Precio = x.Precio,
                                                 //Utilizar propiedad navegacion,
                                                 // para obtener informacion de una clase relacionada
                                                 TipoProducto = x.TipoProducto.Nombre,
                                                 TipoProductoId = x.TipoProductoId

                                             }
                                            );
            var resultado = new ListaPaginada<ProductoDto>();
            resultado.Total = total;
            resultado.Lista = listaProductosDto.ToList();

            return resultado;
        }

        public Task<ProductoDto> GetByIdAsync(int id)
        {
            var consulta = repository.GetAllIncluding(x => x.TipoProducto, x => x.Marca);
            consulta = consulta.Where(x => x.Id == id);

            var consultaProductoDto = consulta
                                    .Select(
                                        x => new ProductoDto()
                                        {
                                            Id = x.Id,
                                            Caducidad = x.Caducidad,
                                            //Utilizar propiedad navegacion,
                                            // para obtener informacion de una clase relacionada
                                            Marca = x.Marca.Nombre,
                                            MarcaId = x.MarcaId,
                                            Nombre = x.Nombre,
                                            Observaciones = x.Observaciones,
                                            Precio = x.Precio,
                                            //Utilizar propiedad navegacion,
                                            // para obtener informacion de una clase relacionada
                                            TipoProducto = x.TipoProducto.Nombre,
                                            TipoProductoId = x.TipoProductoId
                                        }
                                    );

            return Task.FromResult(consultaProductoDto.SingleOrDefault());
        }

        public async Task<ProductoDto> GetByName(string nombre)
        {

            var consulta = repository.GetAll();
            consulta = consulta.Where(x => x.Nombre == nombre);
            var existeNombreProducto = await repository.ExisteNombre(nombre);
            if (!existeNombreProducto)
            {
                throw new ArgumentException($"No existe el nombre {nombre} en Productos");
            }
            var listaProductosDto = consulta
                               .Select(
                                x => new ProductoDto()
                                {
                                    Id = x.Id,
                                    Caducidad = x.Caducidad,
                                    //Utilizar propiedad navegacion,
                                    //para obtener información de una clase relacionada
                                    Marca = x.Marca.Nombre,
                                    MarcaId = x.MarcaId,
                                    Nombre = x.Nombre,
                                    Observaciones = x.Observaciones,
                                    Precio = x.Precio,
                                    //Utilizar propiedad navegacion,
                                    // para obtener informacion de una clase relacionada
                                    TipoProducto = x.TipoProducto.Nombre,
                                    TipoProductoId = x.TipoProductoId

                                }
                               );
            return listaProductosDto.ToList().Single();
        }

        public async Task<ListaPaginada<ProductoDto>> GetListAsync(ProductoListInput input)
        {
            var consulta = repository.GetAllIncluding(x => x.Marca,
                      x => x.TipoProducto);

            //Aplicar filtros
            if (input.TipoProductoId.HasValue)
            {
                consulta = consulta.Where(x => x.TipoProductoId == input.TipoProductoId);
            }

            if (input.MarcaId.HasValue)
            {
                consulta = consulta.Where(x => x.MarcaId == input.MarcaId);
            }

            if (!string.IsNullOrEmpty(input.ValorBuscar))
            {

                //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
                //    x.Codigo.StartsWith(input.ValorBuscar));
                consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar));
            }

            //Ejecuatar linq. Total registros
            var total = consulta.Count();

            //Aplicar paginacion
            consulta = consulta.Skip(input.Offset)
                        .Take(input.Limit);

            //Obtener el listado paginado. (Proyeccion)
            var consulaListaProductosDto = consulta
                                    .Select(
                                        x => new ProductoDto()
                                        {
                                            Id = x.Id,
                                            Caducidad = x.Caducidad,
                                            //Utilizar propiedad navegacion,
                                            // para obtener informacion de una clase relacionada
                                            Marca = x.Marca.Nombre,
                                            MarcaId = x.MarcaId,
                                            Nombre = x.Nombre,
                                            Observaciones = x.Observaciones,
                                            Precio = x.Precio,
                                            //Utilizar propiedad navegacion,
                                            // para obtener informacion de una clase relacionada
                                            TipoProducto = x.TipoProducto.Nombre,
                                            TipoProductoId = x.TipoProductoId
                                        }
                                    );


            var resultado = new ListaPaginada<ProductoDto>();
            resultado.Total = total;
            resultado.Lista = consulaListaProductosDto.ToList();

            return resultado;

        }

        public async Task UpdateAsync(int id, ProductoCrearActualizarDto productoDto)
        {
            var producto = await repository.GetByIdAsync(id);
            if (producto == null)
            {
                throw new ArgumentException($"El producto con el id: {id}, no existe");
            }
            var existeNombreProducto = await repository.ExisteNombre(productoDto.Nombre, id);
            if (existeNombreProducto)
            {
                throw new ArgumentException($"Ya existe una marca con el nombre {producto.Nombre}");
            }
            //mapeo Dto => Entidad
            producto.Nombre = productoDto.Nombre;
            producto.Precio = productoDto.Precio;
            producto.Observaciones = productoDto.Observaciones;
            producto.Caducidad = productoDto.Caducidad;
            producto.MarcaId = productoDto.MarcaId;
            producto.TipoProductoId = productoDto.TipoProductoId;
            await repository.UpdateAsync(producto);

            return;
        }


    }
}