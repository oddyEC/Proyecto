using System.IO.Compression;
using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application
{
    public class ProductoAppService : IProductoAppService
    {
        private readonly IProductoRepository repository;

        public ProductoAppService(IProductoRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto productoDto)
        {
            //Reglas Validaciones... 
            var existeNombreProducto = await repository.ExisteNombre(productoDto.Nombre);
            if (existeNombreProducto)
            {
                throw new ArgumentException($"Ya existe una marca con el nombre {productoDto.Nombre}");
            }

            //Mapeo Dto => Entidad
            var producto = new Producto();
            producto.Nombre = productoDto.Nombre;
            producto.Precio = productoDto.Precio;
            producto.Observaciones = productoDto.Observaciones;
            producto.Caducidad = productoDto.Caducidad;
            producto.MarcaId = productoDto.MarcaId;
            producto.TipoProductoId = productoDto.TipoProductoId;
            //Persistencia objeto
            producto = await repository.AddAsync(producto);
            //await unitOfWork.SaveChangesAsync();

            //Mapeo Entidad => Dto
            var productoCreado = new ProductoDto();
            productoCreado.Nombre = producto.Nombre;
            productoCreado.Precio = producto.Precio;
            productoCreado.Observaciones = producto.Observaciones;
            productoCreado.Caducidad = producto.Caducidad;
            productoCreado.Id = producto.Id;
            productoCreado.MarcaId = producto.MarcaId;
            productoCreado.TipoProductoId = producto.TipoProductoId;


            //TODO: Enviar un correo electronica... 

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
            //await unitOfWork.SaveChangesAsync();

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
            var consulta = repository.GetAll();
            consulta = consulta.Where(x => x.Id == id);
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
            return Task.FromResult(listaProductosDto.SingleOrDefault());
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