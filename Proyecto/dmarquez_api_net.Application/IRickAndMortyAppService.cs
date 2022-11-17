using dmarquez_api_net.Domain;

namespace dmarquez_api_net.Application;

public interface IRickAndMortyAppService
{
    ICollection<ClasePersonaje> GetResult();
}
