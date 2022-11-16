using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Domain.Entidades;
using Curso.ComercioElectronico.Domain.Repository;

namespace Curso.ComercioElectronico.Infraestructure
{
    public class TipoClienteRepository : EfRepository<TipoCliente, string>, ITipoClienteRepository
    {
        public TipoClienteRepository(ComercioElectronicoDbContext context) : base(context)
        {
        }
    }
}