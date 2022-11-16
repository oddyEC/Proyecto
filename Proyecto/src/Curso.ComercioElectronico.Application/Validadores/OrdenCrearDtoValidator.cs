using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Application.Dtos;
using FluentValidation;

namespace Curso.ComercioElectronico.Application.Validadores
{
    public class OrdenCrearDtoValidator : AbstractValidator<OrdenCrearDto>
    {
        public OrdenCrearDtoValidator()
        {
            RuleFor(x => x.Fecha).Must(BeAValidDate).WithMessage("La fecha es requerida");
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}