using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain.Entidades;

namespace Curso.ComercioElectronico.Domain
{
    public class Cliente
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO_32)]
        public string Nombres { get; set; }
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO_12)]
        public string Cedula { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public string TipoClienteId {get; set;}
        public virtual TipoCliente TipoCliente {get; set;}
    }
}