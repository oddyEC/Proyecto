using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Application
{
    public class ListaPaginada<T> where T : class
    {
        public ICollection<T>? Lista { get; set; }

        public long Total { get; set; }
    }
}