using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Domain
{
    public interface ITipoProductoRepository :  IRepository<TipoProducto,int>
    {
        Task<bool> ExisteNombre(string nombre);

        Task<bool> ExisteNombre(string nombre, int idExcluir);
    }
}