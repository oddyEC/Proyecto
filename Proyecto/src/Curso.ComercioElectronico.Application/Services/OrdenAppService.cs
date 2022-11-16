using AutoMapper;
using Curso.ComercioElectronico.Application.Dtos;
using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application
{
    public class OrdenAppService : IOrdenAppService
    {
        private readonly IOrdenRepository ordenRepository;
        private readonly IProductoAppService productoAppService;
        private readonly ILogger<OrdenAppService> logger;
        private readonly IMapper mapper;

        public OrdenAppService(IOrdenRepository ordenRepository, IProductoAppService productoAppService, ILogger<OrdenAppService> logger, IMapper mapper)
        {
            this.ordenRepository = ordenRepository;
            this.productoAppService = productoAppService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<OrdenDto> CreateAsync(OrdenCrearDto ordenDto)
        {
            logger.LogInformation("Crear Orden");
            var orden = new Orden(Guid.NewGuid());
            orden.ClienteId = ordenDto.ClienteId;
            orden.Estado = OrdenEstado.Registrada;
            orden.Fecha = ordenDto.Fecha;
            var observaciones = string.Empty;
            foreach (var item in ordenDto.Items)
            {
                var productoDto = await productoAppService.GetByIdAsync(item.ProductId);
                if (productoDto != null)
                {
                    var ordenItem = new OrdenItem(Guid.NewGuid());
                    ordenItem.Cantidad = item.Cantidad;
                    ordenItem.Precio = productoDto.Precio;
                    ordenItem.ProductId = productoDto.Id;
                    ordenItem.Observaciones = item.Observaciones;
                    //ordenItem.SubTotal = (Cantidad * Precio) - Descuento ??
                    orden.AgregarItem(ordenItem);
                }
                else
                {
                    observaciones += $"El producto {item.ProductId}, no existe";
                }
            }
            orden.Total = orden.Items.Sum(x => x.Cantidad * x.Precio);
            orden.Observaciones = observaciones;

            //3. Persistencias.
            orden = await ordenRepository.AddAsync(orden);
            await ordenRepository.UnitOfWork.SaveChangesAsync();

            return await GetByIdAsync(orden.Id);
        }

        public Task<bool> AnularAsync(Guid odenId)
        {
            throw new NotImplementedException();
        }

        public ListaPaginada<OrdenDto> GetAll(int limit = 10, int offset = 0)
        {
            var consulta = ordenRepository.GetAllIncluding(x => x.Cliente,
                                                           x => x.Items
                                                           );
            var total = consulta.Count();
            var listaOrdenesDto = consulta.Skip(offset)
                                          .Take(limit)
                                          .Select(
                                            x => new OrdenDto()
                                            {
                                                Id = x.Id,
                                                Cliente = x.Cliente.Nombres,
                                                ClienteId = x.ClienteId,

                                                Fecha = x.Fecha,
                                                FechaAnulacion = x.FechaAnulacion,
                                                Total = x.Total,
                                                Items = x.Items.Select(item => new OrdenItemDto()
                                                {
                                                    Cantidad = item.Cantidad,
                                                    Id = item.Id,
                                                    Observaciones = item.Observaciones,
                                                    OrdenId = item.OrdenId,
                                                    Precio = item.Precio,
                                                    ProductId = item.ProductId,
                                                    Product = item.Product.Nombre
                                                }).ToList(),
                                                Observaciones = x.Observaciones,
                                                Estado = x.Estado

                                            }
                                          );
            var resultado = new ListaPaginada<OrdenDto>();
            resultado.Total = total;
            resultado.Lista = listaOrdenesDto.ToList();

            return resultado;

        }

        public Task<OrdenDto> GetByIdAsync(Guid id)
        {
            var consulta = ordenRepository.GetAllIncluding(x => x.Cliente, x => x.Items); //, x => x.Vendedor);
            consulta = consulta.Where(x => x.Id == id);

            var consultaOrdenDto = consulta
                                    .Select(
                                        x => new OrdenDto()
                                        {
                                            //VendedorNombre = $"{x.Vendedor.Nombre} {x.Vendedor.Apellido}", 
                                            Id = x.Id,
                                            Cliente = x.Cliente.Nombres,
                                            ClienteId = x.ClienteId,
                                            Estado = x.Estado,
                                            Fecha = x.Fecha,
                                            FechaAnulacion = x.FechaAnulacion,
                                            Observaciones = x.Observaciones,
                                            Total = x.Total,
                                            Items = x.Items.Select(item => new OrdenItemDto()
                                            {
                                                Cantidad = item.Cantidad,
                                                Id = item.Id,
                                                Observaciones = item.Observaciones,
                                                OrdenId = item.OrdenId,
                                                Precio = item.Precio,
                                                ProductId = item.ProductId,
                                                Product = item.Product.Nombre
                                            }).ToList()
                                        }
                                    );
            return Task.FromResult(consultaOrdenDto.SingleOrDefault());
        }

        public async Task UpdateAsync(Guid id, OrdenActualizarDto ordenDto)
        {
            var orden = await ordenRepository.GetByIdAsync(id);
            if (orden == null)
            {
                throw new ArgumentException($"La orden con el id: {id}, no existe");
            }

            orden.Observaciones = ordenDto.Observaciones;

            orden.Estado = ordenDto.Estado;
            await ordenRepository.UpdateAsync(orden);

            return;
        }
    }
}