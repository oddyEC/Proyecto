using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Application.Dtos
{
    public class TipoClienteCrearActualizarDto
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Password { get; set; }
        public TipoClienteEstado TipoClienteEstado { get; set; }

    }

}