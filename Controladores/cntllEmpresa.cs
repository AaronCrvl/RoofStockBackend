using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using System.Net;
using System.Net.Mime;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class cntllEmpresa : Controller
    {
        private readonly Repositorio.RepoEmpresa repo;

        public cntllEmpresa()
        {
            this.repo = new Repositorio.RepoEmpresa();
        }

        #region HTTP Methods

        // Obter Empresa pelo ID
        [HttpGet("GetEmpresa/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmpresa(long id)
        {
            try
            {
                var empresa = await this.repo.CarregarEmpresa(id);

                if (empresa != null && empresa.ID_EMPRESA > 0)
                    return Ok(empresa);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest("Message: " + e.Message + " StackTrace: " + e.StackTrace);
            }
        }

        // Obter Empresa pelo nome
        [HttpGet("GetEmpresaByName/{name}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmpresaByName(string name)
        {
            try
            {
                var empresa = await this.repo.CarregarEmpresaPorNome(name);

                if (empresa != null && empresa.ID_EMPRESA > 0)
                    return Ok(empresa);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest("Message: " + e.Message + " StackTrace: " + e.StackTrace);
            }
        }

        // Criar uma nova Empresa
        [HttpPost("CreateEmpresa")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] Empresa novaEmpresa)
        {
            try
            {
                // Chama o repositório para criar a empresa
                var empresaCriada = await this.repo.CriarEmpresa(novaEmpresa);

                if (empresaCriada != null && empresaCriada.ID_EMPRESA > 0)
                    return Ok(empresaCriada);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest("Message: " + e.Message + " StackTrace: " + e.StackTrace);
            }
        }

        // Editar dados de uma Empresa
        [HttpPost("EditEmpresa/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditEmpresa(long id, [FromBody] Empresa empresaEditada)
        {
            try
            {
                // Chama o repositório para editar a empresa
                var empresaAtualizada = await this.repo.AlterarEmpresa(id, empresaEditada);

                if (empresaAtualizada != null)
                    return Ok(empresaAtualizada);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest("Message: " + e.Message + " StackTrace: " + e.StackTrace);
            }
        }

        // Desativar uma Empresa
        [HttpPost("DesativarEmpresa/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DesativarEmpresa(long id)
        {
            try
            {
                // Chama o repositório para desativar a empresa
                var resultado = await this.repo.DesativarEmpresa(id);

                if (resultado)
                    return Ok("Empresa desativada com sucesso.");
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest("Message: " + e.Message + " StackTrace: " + e.StackTrace);
            }
        }

        #endregion
    }
}
