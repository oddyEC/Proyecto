using AutoMapper;
using Curso.ComercioElectronico.Application.Dtos;
using Curso.ComercioElectronico.Domain;
namespace Curso.ComercioElectronico.Application.Mapeos
{
    public class ConfiguracionesMapeoProfile :  Profile
    {
        //TipoProductoCrearActualizarDto => TipoProducto
        //TipoProducto => TipoProductoDto
        public ConfiguracionesMapeoProfile()
        {
            CreateMap<TipoProductoCrearActualizarDto, TipoProducto>();
            CreateMap<TipoProducto, TipoProductoDto>();
            CreateMap<ClienteCrearActualizarDto, Cliente>();
            CreateMap<Cliente, ClienteDto>();
            CreateMap<MarcaCrearActualizarDto, Marca>();
            CreateMap<Marca, MarcaDto>();
            CreateMap<OrdenActualizarDto, Orden>();
            CreateMap<OrdenCrearDto, Orden>();
            CreateMap<Orden, OrdenDto>();
            CreateMap<ProductoCrearActualizarDto, Producto>();
            CreateMap<Producto, ProductoDto>();
            //TODO: Agregar otros mapeos que se requieren...

        }
    }
}