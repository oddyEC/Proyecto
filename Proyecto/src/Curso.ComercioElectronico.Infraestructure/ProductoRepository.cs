using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure
{
    public class ProductoRepository : EfRepository<Producto, int>, IProductoRepository
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
        public async Task<ICollection<Producto>> GetListAsync(IList<int> listaIds, bool asNoTracking = true)
        {
            //GetAll, se ejecuta el linq???
            var consulta = GetAll(asNoTracking);

            consulta = consulta.Where(
                    x => listaIds.Contains(x.Id)
                );

            //select * from productos where id in (1,3,4,5)
            return await consulta.ToListAsync();

        }
    }
}