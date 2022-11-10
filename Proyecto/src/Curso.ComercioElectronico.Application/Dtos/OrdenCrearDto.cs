using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Application.Dtos
{
    public class OrdenCrearDto
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public virtual ICollection<OrdenItemCrearActualizarDto> Items { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public string? Observaciones { get; set; }
    }
}