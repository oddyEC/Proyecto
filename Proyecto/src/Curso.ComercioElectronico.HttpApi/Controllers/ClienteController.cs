using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteAppService clienteAppService;

        public ClienteController(IClienteAppService clienteAppService)
        {
            this.clienteAppService = clienteAppService;
        }



        [HttpPost]
        public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteCrearActualizarDto)
        {

            return await clienteAppService.CreateAsync(clienteCrearActualizarDto);

        }

        //TODO: Agregar las otras capacidades del api de clientes..
    }
}