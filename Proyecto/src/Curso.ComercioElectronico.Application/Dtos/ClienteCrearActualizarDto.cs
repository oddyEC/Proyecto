using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application.Dtos
{
    public class ClienteCrearActualizarDto
    {
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
        public string Nombres { get; set; }
    }
}