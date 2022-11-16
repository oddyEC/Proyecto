using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain.Entidades
{
    public class CarroItem
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Producto Producto { get; set; }

        [Required]
        public int CarroId { get; set; }

        public virtual Carro Carro { get; set; }

        [Required]
        public long Cantidad { get; set; }

        public decimal Precio { get; set; }

        public string? Observaciones { get; set; }

        public CarroItemEstado CarroItemEstado{get; set;}
    }
}

    public enum CarroItemEstado
    {

        Agotado = 0,

        Disponible = 1
    }