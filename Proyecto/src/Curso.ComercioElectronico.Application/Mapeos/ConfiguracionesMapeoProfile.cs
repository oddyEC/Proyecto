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

            //TODO: Agregar otros mapeos que se requieren...

        }
    }
}