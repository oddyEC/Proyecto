using AutoMapper;
using Curso.ComercioElectronico.Application.Dtos;
using Curso.ComercioElectronico.Application.Interfaces;
using Curso.ComercioElectronico.Domain.Entidades;
using Curso.ComercioElectronico.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application.Services
{
    public class CarroAppService : ICarroAppService
    {
        private readonly ICarroRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<TipoProductoAppService> logger;

        public CarroAppService(ICarroRepository repository, IMapper mapper, ILogger<TipoProductoAppService> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<CarroDto> CreateAsync(CarroCrearActualizarDto carroDto)
        {
            logger.LogInformation("Crear Carro");
            var carro = mapper.Map<Carro>(carroDto);
            carro = await repository.AddAsync(carro);
            await repository.UnitOfWork.SaveChangesAsync();
            var carroCreado = mapper.Map<CarroDto>(carro);
            return carroCreado;
        }

        public async Task<bool> DeleteAsync(int carroId)
        {
            var carro = await repository.GetByIdAsync(carroId);
            if (carro == null)
            {
                throw new ArgumentException($"El carro con el id: {carroId}, no existe");
            }
            repository.Delete(carro);
            return true;
        }

        public ICollection<CarroDto> GetAll()
        {
            var carroList = repository.GetAll();

            var carroListDto = from c in carroList
                               select new CarroDto(){
                                Id = c.Id,
                                Cliente = c.Cliente,
                                ClienteId = c.ClienteId,
                                Subtotal = c.Subtotal,

                               };
            return carroListDto.ToList();
        }

        public async Task UpdateAsync(int id, CarroCrearActualizarDto carroDto)
        {
            var carro = await repository.GetByIdAsync(id);
            if(carro == null){
                throw new ArgumentException($"El carrito de compra con el id: {id}, no existe");
            }
            carro.Subtotal = carroDto.Subtotal;
            await repository.UpdateAsync(carro);
            await repository.UnitOfWork.SaveChangesAsync();
        }
    }
}