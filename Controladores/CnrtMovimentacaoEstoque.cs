using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Services;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Tags("Movimentação Estoque")]
    [Route("MovimentacaoEstoque")]
    public class MovimentacaoEstoqueController : ControllerBase
    {
        private readonly SrvcMovimentacaoEstoque _movimentacaoService;

        public MovimentacaoEstoqueController(SrvcMovimentacaoEstoque movimentacaoService)
        {
            _movimentacaoService = movimentacaoService;
        }

        [HttpPost("Criar")]
        public async Task<IActionResult> CriarMovimentacao([FromBody] MovimentacaoEstoque movimentacao)
        {
            try
            {
                var result = await _movimentacaoService.CriarMovimentacaoAsync(movimentacao);
                if (result)
                    return Ok("Movimentação criada com sucesso.");
                return BadRequest("Erro ao criar movimentação.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("ObterPorId/{id}")]
        public async Task<IActionResult> ObterMovimentacaoPorId(long id)
        {
            try
            {
                var movimentacao = await _movimentacaoService.CarregarMovimentacaoPorIdAsync(id);
                if (movimentacao != null)
                    return Ok(movimentacao);
                return NotFound("Movimentação não encontrada.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("ListarPorEstoque/{estoqueId}")]
        public async Task<IActionResult> ListarMovimentacoesPorEstoque(long estoqueId)
        {
            try
            {
                var movimentacoes = await _movimentacaoService.ListarMovimentacoesPorEstoqueAsync(estoqueId);
                if (movimentacoes != null)
                    return Ok(movimentacoes);
                return NotFound("Nenhuma movimentação encontrada para o estoque.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPatch("Alterar")]
        public async Task<IActionResult> AlterarMovimentacao([FromBody] MovimentacaoEstoque movimentacao)
        {
            try
            {
                var result = await _movimentacaoService.AlterarMovimentacaoAsync(movimentacao);
                if (result)
                    return Ok("Movimentação alterada com sucesso.");
                return BadRequest("Erro ao alterar movimentação.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> ExcluirMovimentacao(long id)
        {
            try
            {
                var result = await _movimentacaoService.ExcluirMovimentacaoAsync(id);
                if (result)
                    return Ok("Movimentação excluída com sucesso.");
                return NotFound("Movimentação não encontrada.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}
