using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application.Dtos
{
    public class ClienteCrearActualizarDto
    {
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
        public string Nombres { get; set; }
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO_12)]
        public string Cedula { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public string TipoClienteId { get; set; }
    }
}