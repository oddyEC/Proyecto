using System.ComponentModel.DataAnnotations;
namespace Curso.ComercioElectronico.Domain
{
    public class Producto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? Caducidad { get; set; }
        [Required]
        public int MarcaId { get; set; }
        public virtual Marca? Marca { get; set; }
        [Required]
        public int TipoProductoId { get; set; }
        public virtual TipoProducto? TipoProducto { get; set; }
    }
}