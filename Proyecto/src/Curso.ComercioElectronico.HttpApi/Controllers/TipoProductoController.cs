using Microsoft.AspNetCore.Mvc;
using Curso.ComercioElectronico.Application;
namespace Curso.ComercioElectronico.HttpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoProductoController : ControllerBase
    {
        private readonly ITipoProductoService tipoProductoService;
        public TipoProductoController(ITipoProductoService tipoProductoService)
        {
            this.tipoProductoService = tipoProductoService;
        }
        [HttpGet]
        public ICollection<TipoProductoDto> GetAll()
        {

            return tipoProductoService.GetAll();
        }

        [HttpPost]
        public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tproducto)
        {

            return await tipoProductoService.CreateAsync(tproducto);

        }

        [HttpPut]
        public async Task UpdateAsync(int id, TipoProductoCrearActualizarDto tproducto)
        {

            await tipoProductoService.UpdateAsync(id, tproducto);

        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(int tproductoId)
        {

            return await tipoProductoService.DeleteAsync(tproductoId);

        }

    }
}