using Microsoft.AspNetCore.Mvc;
using Curso.ComercioElectronico.Application;

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
        public ListaPaginada<ProductoDto> GetAll()
        {

            return productoService.GetAll();
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