using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain
{
    public class TipoProducto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
        public string? Nombre { get; set; }

        [Required]
        public Guid ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}