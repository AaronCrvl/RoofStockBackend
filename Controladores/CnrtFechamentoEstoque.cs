using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Services;
using System.Threading.Tasks;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CntrlFechamentoEstoque : ControllerBase
    {
        private readonly SrvcFechamentoEstoque _fechamentoEstoqueService;

        public CntrlFechamentoEstoque(SrvcFechamentoEstoque fechamentoEstoqueService)
        {
            _fechamentoEstoqueService = fechamentoEstoqueService;
        }

        [HttpPost("Criar")]
        public async Task<IActionResult> CriarFechamentoEstoque([FromBody] FechamentoEstoque fechamentoEstoque)
        {
            if (fechamentoEstoque == null)
                return BadRequest("Fechamento de estoque inválido.");

            var resultado = await _fechamentoEstoqueService.CriarFechamentoEstoqueAsync(fechamentoEstoque);
            if (resultado)
                return Ok("Fechamento de estoque criado com sucesso.");
            return BadRequest("Erro ao criar fechamento de estoque.");
        }

        [HttpGet("Obter/{id}")]
        public async Task<IActionResult> ObterFechamentoEstoque(long id)
        {
            var fechamentoEstoque = await _fechamentoEstoqueService.CarregarFechamentoEstoquePorIdAsync(id);
            if (fechamentoEstoque != null)
                return Ok(fechamentoEstoque);
            return NotFound("Fechamento de estoque não encontrado.");
        }

        [HttpGet("ObterPorEstoque/{idEstoque}")]
        public async Task<IActionResult> ObterFechamentoPorEstoque(long idEstoque)
        {
            var fechamentoEstoque = await _fechamentoEstoqueService.CarregarFechamentoPorEstoqueAsync(idEstoque);
            if (fechamentoEstoque != null)
                return Ok(fechamentoEstoque);
            return NotFound("Fechamento de estoque não encontrado para o estoque informado.");
        }

        [HttpPut("Alterar")]
        public async Task<IActionResult> AlterarFechamentoEstoque([FromBody] FechamentoEstoque fechamentoEstoque)
        {
            var resultado = await _fechamentoEstoqueService.AlterarFechamentoEstoqueAsync(fechamentoEstoque);
            if (resultado)
                return Ok("Fechamento de estoque alterado com sucesso.");
            return BadRequest("Erro ao alterar fechamento de estoque.");
        }

        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> ExcluirFechamentoEstoque(long id)
        {
            var resultado = await _fechamentoEstoqueService.ExcluirFechamentoEstoqueAsync(id);
            if (resultado)
                return Ok("Fechamento de estoque excluído com sucesso.");
            return NotFound("Fechamento de estoque não encontrado.");
        }

        [HttpPut("Desativar/{id}")]
        public async Task<IActionResult> DesativarFechamentoEstoque(long id)
        {
            var resultado = await _fechamentoEstoqueService.DesativarFechamentoEstoqueAsync(id);
            if (resultado)
                return Ok("Fechamento de estoque desativado com sucesso.");
            return NotFound("Fechamento de estoque não encontrado.");
        }
    }
}