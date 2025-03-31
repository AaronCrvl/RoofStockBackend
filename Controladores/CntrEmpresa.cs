using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Services;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Tags("Empresa")]
    [Route("Empresa")]
    public class CntrlEmpresa : ControllerBase
    {
        private readonly SrvcEmpresa _empresaService;

        public CntrlEmpresa(SrvcEmpresa empresaService)
        {
            _empresaService = empresaService;
        }

        #region Métodos HTTP

        [HttpGet("ObterEmpresa/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterEmpresaPorId(long id)
        {
            try
            {
                var empresa = await _empresaService.CarregarEmpresaPorIdAsync(id);

                if (empresa == null)
                {
                    return NotFound(new { Message = "Empresa não encontrada." });
                }

                return Ok(empresa);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpGet("ObterEmpresaPorNome/{nome}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterEmpresaPorNome(string nome)
        {
            try
            {
                var empresa = await _empresaService.CarregarEmpresaPorNomeAsync(nome);

                if (empresa == null)
                {
                    return NotFound(new { Message = "Empresa não encontrada." });
                }

                return Ok(empresa);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPost("CriarEmpresa")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarEmpresa([FromBody] Empresa novaEmpresa)
        {
            try
            {
                if (novaEmpresa == null)
                {
                    return BadRequest(new { Message = "Dados inválidos." });
                }

                bool sucesso = await _empresaService.CriarEmpresaAsync(novaEmpresa);

                if (!sucesso)
                {
                    return BadRequest(new { Message = "Erro ao criar empresa." });
                }

                return CreatedAtAction(nameof(ObterEmpresaPorId), new { id = novaEmpresa.ID_EMPRESA }, novaEmpresa);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPut("AlterarEmpresa/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterarEmpresa(long id, [FromBody] Empresa empresaAlterada)
        {
            try
            {
                if (empresaAlterada == null || id != empresaAlterada.ID_EMPRESA)
                {
                    return BadRequest(new { Message = "Dados inválidos." });
                }

                bool sucesso = await _empresaService.AlterarEmpresaAsync(empresaAlterada);

                if (!sucesso)
                {
                    return NotFound(new { Message = "Empresa não encontrada." });
                }

                return Ok(empresaAlterada);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpDelete("ExcluirEmpresa/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirEmpresa(long id)
        {
            try
            {
                bool sucesso = await _empresaService.ExcluirEmpresaAsync(id);

                if (!sucesso)
                {
                    return NotFound(new { Message = "Empresa não encontrada." });
                }

                return Ok(new { Message = "Empresa excluída com sucesso." });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPut("DesativarEmpresa/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DesativarEmpresa(long id)
        {
            try
            {
                bool sucesso = await _empresaService.DesativarEmpresaAsync(id);

                if (!sucesso)
                {
                    return NotFound(new { Message = "Empresa não encontrada." });
                }

                return Ok(new { Message = "Empresa desativada com sucesso." });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPut("AtivarEmpresa/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtivarEmpresa(long id)
        {
            try
            {
                bool sucesso = await _empresaService.AtivarEmpresaAsync(id);

                if (!sucesso)
                {
                    return NotFound(new { Message = "Empresa não encontrada." });
                }

                return Ok(new { Message = "Empresa ativada com sucesso." });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        #endregion
    }
}
