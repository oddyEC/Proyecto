using Curso.ComercioElectronico.Domain.Entidades;
using Curso.ComercioElectronico.Domain.Repository;

namespace Curso.ComercioElectronico.Infraestructure
{
    public class CarroRepository : EfRepository<Carro, int>, ICarroRepository
    {
        public CarroRepository(ComercioElectronicoDbContext context) : base(context)
        {
        }
    }
}