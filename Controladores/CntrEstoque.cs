﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Modelos.DTO.Estoque;
using RoofStockBackend.Services;
using System.Net.Mime;
using Serilog;
using RoofStockBackend.Sessão;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Tags("Estoque")]
    [Route("Stock")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class EstoqueController : ControllerBase
    {
        #region Propriedades Privadas
        private readonly SrvcEstoque _estoqueService;
        private readonly Serilog.ILogger _logger;
        #endregion        

        #region Construtor
        public EstoqueController(SrvcEstoque estoqueService, LoggerConfiguration logger)
        {
            _estoqueService = estoqueService;
            _logger = logger.WriteTo.File($"logs/roofLog.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        }
        #endregion                

        #region Métodos HTTP

        [HttpGet("Get")]
        public async Task<IActionResult> ObterEstoquePorId(int id)
        {
            try
            {
                var estoque = await _estoqueService.CarregarEstoquePorIdAsync(id);
                if (estoque == null)
                    return NotFound(new { Message = "Estoque não encontrado." });

                return Ok(estoque);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "Error in EstoqueController.ObterEstoquePorId ", $"StockId: {id}");
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpGet("GetByUser")]
        public async Task<IActionResult> ObterEstoquePorUsuario(int userId)
        {
            try
            {
                SessaoUtils.SetUserId(userId.ToString(), HttpContext);                

                var estoque = await _estoqueService.CarregarEstoquePorUsuario(userId);
                if (estoque == null)
                    return NotFound(new { Message = "Estoque não encontrado." });

                return Ok(estoque);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "Error in EstoqueController.ObterEstoquePorUsuario ", $"StockId: {userId}");
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CriarEstoque([FromBody] EstoqueCadastrarDto novoEstoque)
        {
            try
            {
                if (novoEstoque == null)
                    return BadRequest(new { Message = "Dados inválidos." });

                bool sucesso = await _estoqueService.CriarEstoqueAsync(novoEstoque);
                if (!sucesso)
                    return BadRequest(new { Message = "Erro ao criar estoque." });

                return CreatedAtAction(nameof(ObterEstoquePorId), new { id = novoEstoque.idEstoque }, novoEstoque);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "Error in EstoqueController.CriarEstoque ", $"New Stock: {novoEstoque}");
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPatch("Alter")]
        public async Task<IActionResult> AlterarEstoque(int id, [FromBody] EstoqueAtualizarDto estoqueAlterado)
        {
            try
            {
                bool sucesso = await _estoqueService.AlterarEstoqueAsync(estoqueAlterado);
                if (!sucesso)
                    return NotFound(new { Message = "Estoque não encontrado." });

                return Ok(estoqueAlterado);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "Error in EstoqueController.AlterarEstoque ", $"Stock Alter: {estoqueAlterado}");
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> ExcluirEstoque(int id)
        {
            try
            {
                bool sucesso = await _estoqueService.ExcluirEstoqueAsync(id);
                if (!sucesso)
                    return NotFound(new { Message = "Estoque não encontrado." });

                return Ok(new { Message = "Estoque excluído com sucesso." });
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "Error in EstoqueController.ExcluirEstoque ", $"Stock Delete: {id}");
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpDelete("DeleteItem")]
        public async Task<IActionResult> ExcluirItemEstoque(int idProd)
        {
            try
            {
                bool sucesso = await _estoqueService.ExcluirItemEstoqueAsync(int.Parse(SessaoUtils.GetStockId(HttpContext)), idProd);
                if (!sucesso)
                    return NotFound(new { Message = "Estoque não encontrado." });

                return Ok(new { Message = "Estoque excluído com sucesso." });
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "Error in EstoqueController.ExcluirItemEstoque ", $"Stock Item Delete: {SessaoUtils.GetStockId(HttpContext)} - {idProd}");
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPatch("SetSessionStock")]
        public async Task<IActionResult> AtualizarEstoqueSessao(int idEstoque)
        {
            try
            {
                SessaoUtils.SetStockId(idEstoque.ToString(), HttpContext);
                return Ok(new { Message = "Estoque atualizado na sessão com sucesso." });
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "Error in EstoqueController.AtualizarEstoqueSessao ", $"Não foi possível setar o valor para o estoque {idEstoque}");
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPatch("Unactivate")]
        public async Task<IActionResult> DesativarEstoque(int id)
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

        [HttpPatch("Activate")]
        public async Task<IActionResult> AtivarEstoque(int id)
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
