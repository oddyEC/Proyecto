using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application.Dtos
{
    public class OrdenDto
  {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ClienteId { get; set; }

        public virtual string? Cliente { get; set; }

        public virtual ICollection<OrdenItemDto>? Items { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public DateTime? FechaAnulacion { get; set; }


        [Required]
        public decimal Total { get; set; }

        public string? Observaciones { get; set; }

        [Required]
        public OrdenEstado Estado { get; set; }

    }


    public class OrdenItemDto
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual string? Product { get; set; }

        [Required]
        public Guid OrdenId { get; set; }


        [Required]
        public long Cantidad { get; set; }

        public decimal Precio { get; set; }

        public string? Observaciones { get; set; }
    }
}