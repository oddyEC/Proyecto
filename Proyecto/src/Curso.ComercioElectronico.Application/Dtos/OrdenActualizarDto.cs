using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application.Dtos
{
    public class OrdenActualizarDto
    {
        [Required]
        public OrdenEstado Estado { get; set; }


        public string? Observaciones { get; set; }
    }
    public class OrdenItemCrearActualizarDto {

    [Required]
    public int ProductId {get; set;}
 
   
    [Required]
    public long Cantidad {get;set;}

   
    public string? Observaciones { get;set;}
}
}