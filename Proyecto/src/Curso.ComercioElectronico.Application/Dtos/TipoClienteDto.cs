using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Application.Dtos
{
    public class TipoClienteDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
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