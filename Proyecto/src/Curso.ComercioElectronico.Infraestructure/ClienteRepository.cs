using System.Linq.Expressions;
using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure
{
    public class ClienteRepository : EfRepository<Cliente, Guid>, IClienteRepository
    {
        public ClienteRepository(ComercioElectronicoDbContext context) : base(context)
        {
        }
        public async Task<bool> ExisteNombre(string nombre)
        {

            var resultado = await this._context.Set<Cliente>()
                           .AnyAsync(x => x.Nombres.ToUpper() == nombre.ToUpper());

            return resultado;
        }
    }
}