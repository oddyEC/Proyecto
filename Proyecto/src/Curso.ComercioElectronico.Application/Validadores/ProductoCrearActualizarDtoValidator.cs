using Curso.ComercioElectronico.Application.Dtos;
using FluentValidation;

namespace Curso.ComercioElectronico.Application.Validadores
{
    public class ProductoCrearActualizarDtoValidator : AbstractValidator<ProductoCrearActualizarDto>
    {
        public ProductoCrearActualizarDtoValidator()
        {
            RuleFor(x => x.Precio).GreaterThan(0M);
        }
    }
}