using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Estoque.Produto;
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
        #region Propriedades Privadas
        private readonly SrvcEstoque _estoqueService;
        #endregion        

        #region Construtor
        public EstoqueController(SrvcEstoque estoqueService)
        {
            _estoqueService = estoqueService;
        }
        #endregion        

        #region Métodos HTTP

        #region Métodos de Estoque

        [HttpGet("ObterEstoque/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ObterEstoquePorId(int id)
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

        [HttpGet("ObterEstoquePorUsuario")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ObterEstoquePorUsuario(int idUsuario, int idEmpresa)
        {
            try
            {
                var estoque = await _estoqueService.CarregarEstoquePorUsuario(idUsuario, idEmpresa);
                if (estoque == null)
                    return NotFound(new { Message = "Estoque não encontrado." });

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
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

        [HttpPatch("AlterarEstoque/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AlterarEstoque(int id, [FromBody] Estoque estoqueAlterado)
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ExcluirEstoque(int id)
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

        [HttpPatch("DesativarEstoque/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
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

        [HttpPatch("AtivarEstoque/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
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

        #region Métodos de Produto

        [HttpGet("ObterProdutosEstoque")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ObterProdutosEstoque(int idEstoque)
        {
            try
            {
                var produtosDto = await _estoqueService.CarregarProdutosEstoque(idEstoque);
                if (produtosDto.Count() <= 0)                
                    return NotFound(new { Message = "Sem produtos no estoque indicado." });                

                return Ok(produtosDto);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPost("CadastrarProduto")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CadastrarProduto([FromBody] ProdutoCadastrarDto produtoDto)
        {
            try
            {
                var sucesso = await _estoqueService.CadastrarProdutoAsync(produtoDto);
                if (!sucesso)
                    return BadRequest(new { Message = "Não foi possível cadastrar o produto." });

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPatch("AtualizarProduto")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AtualizarProduto([FromBody] ProdutoAtualizarDto produtoDto)
        {
            try
            {
                var prod = await _estoqueService.AlterarProdutoAsync(produtoDto);
                if (prod.idProduto <0 )
                    return BadRequest(new { Message = $"Não foi possível atualizar o produto {produtoDto.nomeProduto}." });

                return Ok(prod);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpDelete("ExcluirProduto")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ExcluirProduto(int idProduto)
        {
            try
            {
                var sucesso = await _estoqueService.ExcluirProdutoAsync(idProduto);
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

        #endregion
    }
}
