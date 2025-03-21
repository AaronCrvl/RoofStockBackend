using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Services;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CntrlMarca : ControllerBase
    {
        private readonly SrvcMarca _marcaService;

        public CntrlMarca(SrvcMarca marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpPost("CriarMarca")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CriarMarca([FromBody] Marca marca)
        {
            try
            {
                var sucesso = await _marcaService.CriarMarcaAsync(marca);
                if (sucesso)
                {
                    return Ok("Marca criada com sucesso!");
                }
                else
                {
                    return BadRequest("Erro ao criar marca.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("CarregarMarca/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CarregarMarca(long id)
        {
            try
            {
                var marca = await _marcaService.CarregarMarcaPorIdAsync(id);
                if (marca != null)
                {
                    return Ok(marca);
                }
                else
                {
                    return NotFound("Marca não encontrada.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("CarregarMarcaPorNome/{nome}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CarregarMarcaPorNome(string nome)
        {
            try
            {
                var marca = await _marcaService.CarregarMarcaPorNomeAsync(nome);
                if (marca != null)
                {
                    return Ok(marca);
                }
                else
                {
                    return NotFound("Marca não encontrada.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPut("AlterarMarca/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AlterarMarca(long id, [FromBody] Marca marca)
        {
            try
            {
                marca.ID_MARCA = id;
                var sucesso = await _marcaService.AlterarMarcaAsync(marca);
                if (sucesso)
                {
                    return Ok("Marca alterada com sucesso!");
                }
                else
                {
                    return BadRequest("Erro ao alterar marca.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpDelete("ExcluirMarca/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ExcluirMarca(long id)
        {
            try
            {
                var sucesso = await _marcaService.ExcluirMarcaAsync(id);
                if (sucesso)
                {
                    return Ok("Marca excluída com sucesso!");
                }
                else
                {
                    return NotFound("Marca não encontrada.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}
