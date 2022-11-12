using Microsoft.AspNetCore.Mvc;
using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Application.Dtos;

namespace Curso.ComercioElectronico.HttpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductoAppService productoService;
        public ProductController(IProductoAppService productoService)
        {
            this.productoService = productoService;
        }
        [HttpGet]
        public ListaPaginada<ProductoDto> GetAll(int limit = 10, int offset = 0)
        {

            return productoService.GetAll(limit, offset);

        }
        [HttpGet("list")]
        public Task<ListaPaginada<ProductoDto>> GetListAsync([FromQuery] ProductoListInput input)
        {
            return productoService.GetListAsync(input);
        }
        [HttpGet("{id}")]
        public async Task<ProductoDto> GetByIdAsync(int id)
        {
            return await productoService.GetByIdAsync(id);
        }
        [HttpGet("{name}")]
        public async Task<ProductoDto> GetByName(string name)
        {

            return await productoService.GetByName(name);
        }

        [HttpPost]
        public async Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto producto)
        {

            return await productoService.CreateAsync(producto);

        }

        [HttpPut]
        public async Task UpdateAsync(int id, ProductoCrearActualizarDto producto)
        {

            await productoService.UpdateAsync(id, producto);

        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(int productoId)
        {

            return await productoService.DeleteAsync(productoId);

        }
    }
}