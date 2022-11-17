using dmarquez_api_net.Domain;

namespace dmarquez_api_net.Application;

public class RickAndMortyAppService : IRickAndMortyAppService
{
    private readonly IClienteRest clienteRest;
    public RickAndMortyAppService(IClienteRest clienteRest)
    {
        this.clienteRest=clienteRest;
    }
    public ICollection<ClasePersonaje> GetResult()
    {
        var rickAndMorty=clienteRest.GetData();
        var personajes=rickAndMorty.results.Select(x=>new ClasePersonaje(){Id=x.id,Name=x.name}).ToList();

        return personajes;
    }
}
