using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Services;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Route("Estoque")]
    public class EstoqueController : ControllerBase
    {
        private readonly SrvcEstoque _estoqueService;

        public EstoqueController(SrvcEstoque estoqueService)
        {
            _estoqueService = estoqueService;
        }

        #region Métodos HTTP

        [HttpGet("ObterEstoque/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterEstoquePorId(long id)
        {
            try
            {
                var estoque = await _estoqueService.CarregarEstoquePorIdAsync(id);

                if (estoque == null)
                {
                    return NotFound(new { Message = "Estoque não encontrado." });
                }

                return Ok(estoque);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpGet("ObterEstoquePorNome/{nomeEstoque}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterEstoquePorNome(string nomeEstoque)
        {
            try
            {
                var estoque = await _estoqueService.CarregarEstoquePorNomeAsync(nomeEstoque);

                if (estoque == null)
                {
                    return NotFound(new { Message = "Estoque não encontrado." });
                }

                return Ok(estoque);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPost("CriarEstoque")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarEstoque([FromBody] Estoque novoEstoque)
        {
            try
            {
                if (novoEstoque == null)
                {
                    return BadRequest(new { Message = "Dados inválidos." });
                }

                bool sucesso = await _estoqueService.CriarEstoqueAsync(novoEstoque);

                if (!sucesso)
                {
                    return BadRequest(new { Message = "Erro ao criar estoque." });
                }

                return CreatedAtAction(nameof(ObterEstoquePorId), new { id = novoEstoque.ID_ESTOQUE }, novoEstoque);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPut("AlterarEstoque/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterarEstoque(long id, [FromBody] Estoque estoqueAlterado)
        {
            try
            {
                if (estoqueAlterado == null || id != estoqueAlterado.ID_ESTOQUE)
                {
                    return BadRequest(new { Message = "Dados inválidos." });
                }

                bool sucesso = await _estoqueService.AlterarEstoqueAsync(estoqueAlterado);

                if (!sucesso)
                {
                    return NotFound(new { Message = "Estoque não encontrado." });
                }

                return Ok(estoqueAlterado);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpDelete("ExcluirEstoque/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirEstoque(long id)
        {
            try
            {
                bool sucesso = await _estoqueService.ExcluirEstoqueAsync(id);

                if (!sucesso)
                {
                    return NotFound(new { Message = "Estoque não encontrado." });
                }

                return Ok(new { Message = "Estoque excluído com sucesso." });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPut("DesativarEstoque/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DesativarEstoque(long id)
        {
            try
            {
                bool sucesso = await _estoqueService.DesativarEstoqueAsync(id);

                if (!sucesso)
                {
                    return NotFound(new { Message = "Estoque não encontrado." });
                }

                return Ok(new { Message = "Estoque desativado com sucesso." });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPut("AtivarEstoque/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtivarEstoque(long id)
        {
            try
            {
                bool sucesso = await _estoqueService.AtivarEstoqueAsync(id);

                if (!sucesso)
                {
                    return NotFound(new { Message = "Estoque não encontrado." });
                }

                return Ok(new { Message = "Estoque ativado com sucesso." });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        #endregion
    }
}
