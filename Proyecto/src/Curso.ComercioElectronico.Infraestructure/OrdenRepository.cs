using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Domain.Repository;

namespace Curso.ComercioElectronico.Infraestructure
{
    public class OrdenRepository : EfRepository<Orden, Guid>, IOrdenRepository
    {
        public OrdenRepository(ComercioElectronicoDbContext context) : base(context)
        {
        }
    }
}