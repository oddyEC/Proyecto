using dmarquez_api_net.Application;
using dmarquez_api_net.Domain;
using Microsoft.AspNetCore.Mvc;

namespace dmarquez_api_net.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RickAndMortyController:ControllerBase
{
    private readonly IRickAndMortyAppService rickAndMortyAppService;

    public RickAndMortyController(IRickAndMortyAppService rickAndMortyAppService)
    {
        this.rickAndMortyAppService=rickAndMortyAppService;
    }

    [HttpGet("characters")]
    public ICollection<ClasePersonaje> obtenerPersonajes()
    {
        return rickAndMortyAppService.GetResult();
    }

}
