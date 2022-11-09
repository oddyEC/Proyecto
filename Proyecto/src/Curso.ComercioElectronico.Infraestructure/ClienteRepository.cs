using System.Linq.Expressions;
using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure
{
    public class ClienteRepository : EfRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ComercioElectronicoDbContext context) : base(context)
        {
        }

    }
}