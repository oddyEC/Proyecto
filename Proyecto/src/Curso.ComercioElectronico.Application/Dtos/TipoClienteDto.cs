using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application.Dtos
{
    public class TipoClienteDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO_12)]
        public string Usuario { get; set; }
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO_8)]
        public string Password { get; set; }
        public TipoClienteEstado TipoClienteEstado { get; set; }

    }
    public enum TipoClienteEstado
    {
        Inactivo = 0,
        Regular = 1,
        Socio = 2,
    }
}