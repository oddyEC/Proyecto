using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Application.Dtos;

namespace Curso.ComercioElectronico.Application.Interfaces
{
    public interface ICarroAppService
    {
        ICollection<CarroDto> GetAll();

        Task<CarroDto> CreateAsync(CarroCrearActualizarDto carroDto);

        Task UpdateAsync(int id, CarroCrearActualizarDto carroDto);

        Task<bool> DeleteAsync(int carroId);
    }
}