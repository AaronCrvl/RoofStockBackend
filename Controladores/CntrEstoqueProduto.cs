using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Services;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using RoofStockBackend.Modelos.DTO.Produto;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Tags("Estoque Produto")]
    [Route("Product")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CntrlEstoqueProduto : ControllerBase
    {
        #region Propriedades Privadas
        private readonly SrvcEstoqueProduto _estoqueProdutoService;        
        #endregion        

        #region Construtor
        public CntrlEstoqueProduto(SrvcEstoqueProduto estoqueProdutoService)
        {
            _estoqueProdutoService = estoqueProdutoService;            
        }
        #endregion

        #region Métodos de HTTP                
        [HttpGet("GetByStock")]
        public async Task<IActionResult> ObterProdutosEstoque(int stockId)
        {
            try
            {
                var produtosDto = await _estoqueProdutoService.CarregarProdutosEstoqueAsync(stockId);
                if (produtosDto.Count() <= 0)
                    return NotFound(new { Message = "Sem produtos no estoque indicado." });

                return Ok(produtosDto);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CadastrarProduto([FromBody] ProdutoCadastrarDto produtoDto)
        {
            try
            {
                var sucesso = await _estoqueProdutoService.CadastrarProdutoAsync(produtoDto);
                if (!sucesso)
                    return BadRequest(new { Message = "Não foi possível cadastrar o produto." });

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPatch("Alter")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProdutoAtualizarDto produtoDto)
        {
            try
            {
                var prod = await _estoqueProdutoService.AlterarProdutoAsync(id, produtoDto);
                if (prod.idProduto < 0)
                    return BadRequest(new { Message = $"Não foi possível atualizar o produto {produtoDto.nomeProduto}." });

                return Ok(prod);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            try
            {
                var sucesso = await _estoqueProdutoService.ExcluirProdutoAsync(id);
                if (!sucesso)
                    return BadRequest(new { Message = "Não foi possível cadastrar o produto." });

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }
        #endregion
    }
}
