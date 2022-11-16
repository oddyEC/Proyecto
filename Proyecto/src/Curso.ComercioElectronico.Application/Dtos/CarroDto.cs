using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Domain.Entidades;

namespace Curso.ComercioElectronico.Application.Dtos
{
    public class CarroDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<CarroItem> Items { get; set; } = new List<CarroItem>();
        public decimal Subtotal { get; set; }
    }
}