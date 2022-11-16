using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Application.Dtos;
using FluentValidation;

namespace Curso.ComercioElectronico.Application.Validadores
{
    public class ClienteCrearActualizarDtoValidator : AbstractValidator<ClienteCrearActualizarDto>
    {
        public ClienteCrearActualizarDtoValidator()
        {
            RuleFor(x => x.Cedula).Must(x => x.ToString().Length == 10);
        }
    }
}