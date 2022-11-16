using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Domain.Entidades;

namespace Curso.ComercioElectronico.Application.Dtos
{
    public class CarroCrearActualizarDto
    {
        [Required]
        public Guid ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<CarroItem> Items { get; set; } = new List<CarroItem>();
        public decimal Subtotal { get; set; }
    }
}