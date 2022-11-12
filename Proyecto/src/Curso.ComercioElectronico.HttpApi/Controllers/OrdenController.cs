using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenAppService ordenAppService;

        public OrdenController(IOrdenAppService ordenAppService)
        {
            this.ordenAppService = ordenAppService;
        }

        [HttpGet]
        public ListaPaginada<OrdenDto> GetAll(int limit = 10, int offset = 0)
        {

            return ordenAppService.GetAll(limit, offset);

        }

        [HttpGet("{id}")]
        public async Task<OrdenDto> GetByIdAsync(Guid id)
        {
            return await ordenAppService.GetByIdAsync(id);
        }




        [HttpPost]
        public async Task<OrdenDto> CreateAsync(OrdenCrearDto orden)
        {

            return await ordenAppService.CreateAsync(orden);

        }

        [HttpPut]
        public async Task UpdateAsync(Guid id, OrdenActualizarDto orden)
        {

            await ordenAppService.UpdateAsync(id, orden);

        }

        [HttpDelete]
        public async Task<bool> AnularAsync(Guid ordenId)
        {

            return await ordenAppService.AnularAsync(ordenId);

        }
    }
}