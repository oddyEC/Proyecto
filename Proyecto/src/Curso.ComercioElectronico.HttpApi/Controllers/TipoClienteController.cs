using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Application.Dtos;
using Curso.ComercioElectronico.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoClienteController : ControllerBase
    {
        private readonly ITipoClienteAppService tipoClienteAppService;

        public TipoClienteController(ITipoClienteAppService tipoClienteAppService)
        {
            this.tipoClienteAppService = tipoClienteAppService;
        }
        
        [HttpGet]
        public ListaPaginada<TipoClienteDto> GetAll(int limit = 10, int offset = 0)
        {

            return tipoClienteAppService.GetAll(limit, offset);
        }

        [HttpPost]
        public async Task<TipoClienteDto> CreateAsync(TipoClienteCrearActualizarDto tcliente)
        {

            return await tipoClienteAppService.CreateAsync(tcliente);

        }

        [HttpPut]
        public async Task UpdateAsync(string id, TipoClienteCrearActualizarDto tcliente)
        {

            await tipoClienteAppService.UpdateAsync(id, tcliente);

        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(string tclienteId)
        {

            return await tipoClienteAppService.DeleteAsync(tclienteId);

        }
    }
}