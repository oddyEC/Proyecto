using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain
{
    public class Cliente
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
        public string Nombres { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public TipoProducto TipoProducto {get; set;}
    }
}