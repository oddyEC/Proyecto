using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Domain.Entidades;
using Curso.ComercioElectronico.Domain.Repository;

namespace Curso.ComercioElectronico.Infraestructure
{
    public class CarroItemRepository : EfRepository<CarroItem, int>, ICarroItemRepository
    {
        public CarroItemRepository(ComercioElectronicoDbContext context) : base(context)
        {
        }
    }
}