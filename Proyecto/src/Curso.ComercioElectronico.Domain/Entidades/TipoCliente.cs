using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Domain.Entidades
{
    public class TipoCliente
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Usuario {get; set;}
        [Required]
        public string Password{get; set;}
        public TipoClienteEstado TipoClienteEstado { get; set; }

    }
    public enum TipoClienteEstado
    {
        Inactivo = 0,
        Regular = 1,
        Socio = 2,
    }
}