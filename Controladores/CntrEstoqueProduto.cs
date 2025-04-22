using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Services;
using System.Threading.Tasks;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Tags("Estoque Produto")]
    [Route("EstoqueProduto")]
    public class CntrlEstoqueProduto : ControllerBase
    {
        private readonly SrvcEstoqueProduto _estoqueProdutoService;

        public CntrlEstoqueProduto(SrvcEstoqueProduto estoqueProdutoService)
        {
            _estoqueProdutoService = estoqueProdutoService;
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarEstoqueProduto([FromBody] EstoqueProduto estoqueProduto)
        {
            if (estoqueProduto == null)
                return BadRequest("Produto ou estoque inválido.");

            var resultado = await _estoqueProdutoService.AdicionarEstoqueProdutoAsync(estoqueProduto);
            if (resultado)
                return Ok("Produto adicionado ao estoque.");
            return BadRequest("Erro ao adicionar produto ao estoque.");
        }

        [HttpGet("Obter/{estoqueId}/{produtoId}")]
        public async Task<IActionResult> ObterEstoqueProduto(int estoqueId, int produtoId)
        {
            var estoqueProduto = await _estoqueProdutoService.CarregarEstoqueProdutoPorIdAsync(estoqueId, produtoId);
            if (estoqueProduto != null)
                return Ok(estoqueProduto);
            return NotFound("Produto não encontrado no estoque.");
        }

        [HttpGet("Listar/{estoqueId}")]
        public async Task<IActionResult> ListarEstoqueProdutos(int estoqueId)
        {
            var produtos = await _estoqueProdutoService.ListarEstoqueProdutosPorEstoqueAsync(estoqueId);
            if (produtos.Any())
                return Ok(produtos);
            return NotFound("Nenhum produto encontrado para o estoque informado.");
        }

        [HttpPut("Alterar")]
        public async Task<IActionResult> AlterarEstoqueProduto([FromBody] EstoqueProduto estoqueProduto)
        {
            var resultado = await _estoqueProdutoService.AlterarEstoqueProdutoAsync(estoqueProduto);
            if (resultado)
                return Ok("Produto alterado com sucesso.");
            return BadRequest("Erro ao alterar produto.");
        }

        [HttpDelete("Excluir/{estoqueId}/{produtoId}")]
        public async Task<IActionResult> ExcluirEstoqueProduto(int estoqueId, int produtoId)
        {
            var resultado = await _estoqueProdutoService.ExcluirEstoqueProdutoAsync(estoqueId, produtoId);
            if (resultado)
                return Ok("Produto excluído do estoque.");
            return NotFound("Produto não encontrado para excluir.");
        }

        [HttpPut("Desativar/{estoqueId}/{produtoId}")]
        public async Task<IActionResult> DesativarEstoqueProduto(int estoqueId, int produtoId)
        {
            var resultado = await _estoqueProdutoService.DesativarEstoqueProdutoAsync(estoqueId, produtoId);
            if (resultado)
                return Ok("Produto desativado no estoque.");
            return NotFound("Produto não encontrado para desativar.");
        }
    }
}
