using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain.Entidades;


public class Carro
{

    [Required]
    public int Id { get; set; }
    [Required]
    public Guid ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; }

    public virtual ICollection<CarroItem> Items { get; set; } = new List<CarroItem>();
    public decimal Subtotal { get; set; }

    public void AgregarItem(CarroItem item)
    {

        item.Carro = this;
        Items.Add(item);
    }

}

