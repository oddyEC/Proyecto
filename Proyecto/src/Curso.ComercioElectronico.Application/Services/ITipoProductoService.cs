using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Application
{
    public interface ITipoProductoService
    {
        ICollection<TipoProductoDto> GetAll();

        Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tproducto);

        Task UpdateAsync(int id, TipoProductoCrearActualizarDto tproducto);

        Task<bool> DeleteAsync(int tproductoId);
    }
}