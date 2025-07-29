using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Fechamento_Estoque;
using RoofStockBackend.Services;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Tags("Fechamento Estoque")]
    [Route("StockClosure")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CntrlFechamentoEstoque : ControllerBase
    {
        private readonly SrvcFechamentoEstoque _fechamentoEstoqueService;

        public CntrlFechamentoEstoque(SrvcFechamentoEstoque fechamentoEstoqueService)
        {
            _fechamentoEstoqueService = fechamentoEstoqueService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CriarFechamentoEstoque([FromBody] FechamentoEstoqueCriarDto fechamentoEstoque)
        {
            if (fechamentoEstoque == null)
                return BadRequest("Fechamento de estoque inválido.");

            var resultado = await _fechamentoEstoqueService.CriarFechamentoEstoqueAsync(fechamentoEstoque);
            if (resultado)
                return Ok("Fechamento de estoque criado com sucesso.");
            return BadRequest("Erro ao criar fechamento de estoque.");
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> ObterFechamentoEstoque(int id)
        {
            var fechamentoEstoque = await _fechamentoEstoqueService.CarregarFechamentoEstoquePorIdAsync(id);
            if (fechamentoEstoque != null)
                return Ok(fechamentoEstoque);
            return NotFound("Fechamento de estoque não encontrado.");
        }

        [HttpGet("GetByStock")]
        public async Task<IActionResult> ObterFechamentoPorEstoque(int idEstoque)
        {
            var fechamentoEstoque = await _fechamentoEstoqueService.CarregarFechamentoPorEstoqueAsync(idEstoque);
            if (fechamentoEstoque != null)
                return Ok(fechamentoEstoque);
            return NotFound("Fechamento de estoque não encontrado para o estoque informado.");
        }

        [HttpPatch("Alter")]
        public async Task<IActionResult> AlterarFechamentoEstoque([FromBody] FechamentoEstoqueAtualizarDto fechamentoEstoque)
        {           
            var resultado = await _fechamentoEstoqueService.AlterarFechamentoEstoqueAsync(fechamentoEstoque);
            if (resultado)
                return Ok("Fechamento de estoque alterado com sucesso.");
            return BadRequest("Erro ao alterar fechamento de estoque.");
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> ExcluirFechamentoEstoque(int id)
        {
            var resultado = await _fechamentoEstoqueService.ExcluirFechamentoEstoqueAsync(id);
            if (resultado)
                return Ok("Fechamento de estoque excluído com sucesso.");
            return NotFound("Fechamento de estoque não encontrado.");
        }
    }
}