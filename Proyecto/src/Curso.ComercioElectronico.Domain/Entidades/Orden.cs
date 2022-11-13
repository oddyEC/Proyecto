using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Curso.ComercioElectronico.Domain
{
    public class Orden
    {
        public Orden(Guid id)
        {
            this.Id = id;
        }
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<OrdenItem> Items { get; set; } = new List<OrdenItem>();

        [Required]
        public DateTime Fecha { get; set; }

        public DateTime? FechaAnulacion { get; set; }


        [Required]
        public decimal Total { get; set; }

        [Required]
        public OrdenEstado Estado { get; set; }

        public string? Observaciones { get; set; }

        public void AgregarItem(OrdenItem item)
        {

            item.Orden = this;
            Items.Add(item);
        }
    }
    public enum OrdenEstado
    {

        Anulada = 0,

        Registrada = 1,

        Procesada = 2,

        Entregada = 3
    }
}