using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Movimentação;
using RoofStockBackend.Modelos.DTO.Movimentação_Estoque;
using RoofStockBackend.Services;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Tags("Movimentação Estoque")]
    [Route("StockTransaction")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class MovimentacaoEstoqueController : ControllerBase
    {
        #region Propriedades Privadas
        private readonly SrvcMovimentacaoEstoque _movimentacaoService;
        #endregion

        #region Construtor
        public MovimentacaoEstoqueController(SrvcMovimentacaoEstoque movimentacaoService)
        {
            _movimentacaoService = movimentacaoService;
        }
        #endregion

        #region Métodos HTTP
        [HttpPost("Create")]
        public async Task<IActionResult> CriarMovimentacao([FromBody] MovimentacaoEstoqueCriarDto movimentacao)
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

        [HttpGet("GetByStock")]
        public async Task<IActionResult> ListarMovimentacoesPorEstoque(long id)
        {
            try
            {
                var movimentacoes = await _movimentacaoService.ListarMovimentacoesPorEstoqueAsync(id);
                if (movimentacoes != null)
                    return Ok(movimentacoes);
                return NotFound("Nenhuma movimentação encontrada para o estoque.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPatch("Alter")]
        public async Task<IActionResult> AlterarMovimentacao([FromBody] MovimentacaoEstoqueAtualizarDto movimentacao)
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

        [HttpDelete("Delete")]
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
        #endregion        
    }
}
