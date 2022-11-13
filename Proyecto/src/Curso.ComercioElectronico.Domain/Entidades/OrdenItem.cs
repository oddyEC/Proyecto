using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain
{
    public class OrdenItem
    {

        public OrdenItem(Guid id)
        {
            this.Id = id;
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Producto Product { get; set; }

        [Required]
        public Guid OrdenId { get; set; }

        public virtual Orden Orden { get; set; }

        [Required]
        public long Cantidad { get; set; }

        public decimal Precio { get; set; }

        public string? Observaciones { get; set; }
    }
}