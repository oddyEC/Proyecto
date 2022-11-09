using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure
{
    public class ProductoRepository : EfRepository<Producto>, IProductoRepository
    {
        //Persistencia
        public ProductoRepository(ComercioElectronicoDbContext context) : base(context)
        {
        }
        public async Task<bool> ExisteNombre(string nombre)
        {
            var resultado = await this._context.Set<Producto>()
               .AnyAsync(x => x.Nombre.ToUpper() == nombre.ToUpper());

            return resultado;
        }

        public async Task<bool> ExisteNombre(string nombre, int idExcluir)
        {
            var query = this._context.Set<Producto>()
               .Where(x => x.Id != idExcluir)
               .Where(x => x.Nombre.ToUpper() == nombre.ToUpper())
               ;

            var resultado = await query.AnyAsync();

            return resultado;
        }
        public async Task<List<Producto>> GetAllByIdAsync(List<int> indices){
            List<Producto> lista = new List<Producto>();
            foreach (var ind in indices)
            {
                var productoEntidad = await this.GetByIdAsync(ind);
                if (productoEntidad != null)        
                {
                    lista.Add(productoEntidad);
                }
            }
            return lista;
        }
    }
}