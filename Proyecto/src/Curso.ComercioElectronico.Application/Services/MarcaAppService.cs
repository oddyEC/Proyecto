using AutoMapper;
using Curso.ComercioElectronico.Domain;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;



public class MarcaAppService : IMarcaAppService
{
    private readonly IMarcaRepository marcaRepository;
    private readonly IMapper mapper;
    private readonly ILogger<TipoProductoAppService> logger;

    public MarcaAppService(ITipoProductoRepository tipoProductoRepository, IMapper mapper, ILogger<TipoProductoAppService> logger)
    {
        this.marcaRepository = marcaRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    //private readonly IUnitOfWork unitOfWork;



    public async Task<MarcaDto> CreateAsync(MarcaCrearActualizarDto marcaDto)
    {
        logger.LogInformation("Crear Marca");

        //Reglas Validaciones... 
        var existeNombreMarca = await marcaRepository.ExisteNombre(marcaDto.Nombre);
        if (existeNombreMarca)
        {
            throw new ArgumentException($"Ya existe una marca con el nombre {marcaDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        var marca = mapper.Map<Marca>(marcaDto);
        

        //Persistencia objeto
        marca = await marcaRepository.AddAsync(marca);
        await marcaRepository.UnitOfWork.SaveChangesAsync();
        //await unitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        // var marcaCreada = new MarcaDto();
        // marcaCreada.Nombre = marca.Nombre;
        // marcaCreada.Id = marca.Id;
        var marcaCreada = mapper.Map<MarcaDto>(marca);

        //TODO: Enviar un correo electronica... 

        return marcaCreada;
    }

    public async Task UpdateAsync(int id, MarcaCrearActualizarDto marcaDto)
    {
        var marca = await marcaRepository.GetByIdAsync(id);
        if (marca == null)
        {
            throw new ArgumentException($"La marca con el id: {id}, no existe");
        }

        var existeNombreMarca = await marcaRepository.ExisteNombre(marcaDto.Nombre, id);
        if (existeNombreMarca)
        {
            throw new ArgumentException($"Ya existe una marca con el nombre {marcaDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        marca.Nombre = marcaDto.Nombre;

        //Persistencia objeto
        await marcaRepository.UpdateAsync(marca);
        //await unitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int marcaId)
    {
        //Reglas Validaciones... 
        var marca = await marcaRepository.GetByIdAsync(marcaId);
        if (marca == null)
        {
            throw new ArgumentException($"La marca con el id: {marcaId}, no existe");
        }

        marcaRepository.Delete(marca);
        await marcaRepository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<MarcaDto> GetAll()
    {
        var marcaList = marcaRepository.GetAll();

        var marcaListDto = from m in marcaList
                           select new MarcaDto()
                           {
                               Id = m.Id,
                               Nombre = m.Nombre
                           };

        return marcaListDto.ToList();
    }


}
