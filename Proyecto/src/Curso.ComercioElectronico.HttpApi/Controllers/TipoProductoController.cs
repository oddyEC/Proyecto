using Microsoft.AspNetCore.Mvc;
using Curso.ComercioElectronico.Application;
namespace Curso.ComercioElectronico.HttpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoProductoController : ControllerBase
    {

        private readonly ITipoProductoService tipoProductoAppService;

        public TipoProductoController(ITipoProductoService tipoProductoAppService)
        {
            this.tipoProductoAppService = tipoProductoAppService;
        }

        [HttpGet]
        public ListaPaginada<TipoProductoDto> GetAll(int limit = 10, int offset = 0)
        {

            return tipoProductoAppService.GetAll(limit, offset);
        }

        [HttpPost]
        public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto marca)
        {

            return await tipoProductoAppService.CreateAsync(marca);

        }

        [HttpPut]
        public async Task UpdateAsync(int id, TipoProductoCrearActualizarDto marca)
        {

            await tipoProductoAppService.UpdateAsync(id, marca);

        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(int marcaId)
        {

            return await tipoProductoAppService.DeleteAsync(marcaId);

        }
    }
}