using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Services;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using RoofStockBackend.Modelos.DTO.Usuario;
using RoofStockBackend.Sessão;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Tags("Usuario")]
    [Route("User")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CntrUsuario : ControllerBase
    {
        #region Propriedades Privadas
        private readonly SrvcUsuario _usuarioService;
        #endregion

        #region Construtores
        public CntrUsuario(SrvcUsuario usuarioService)
        {
            _usuarioService = usuarioService;
        }
        #endregion

        #region Métodos HTTP

        [HttpGet("GetById")]    
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ObterUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioService.CarregarUsuarioPorIdAsync(id);
                if (usuario != null)
                    return Ok(usuario);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest($"Mensagem: {e.Message} StackTrace: {e.StackTrace}");
            }
        }

        [HttpGet("ObterUsuarioPorUsername/{username}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ObterUsuario(string username)
        {
            try
            {
                var usuario = await _usuarioService.CarregarUsuarioPorLoginAsync(username);
                if (usuario != null)
                    return Ok(usuario);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest($"Mensagem: {e.Message} StackTrace: {e.StackTrace}");
            }
        }

        [HttpPost("Create")]       
        public async Task<IActionResult> Create([FromBody] UsuarioCriarDto novoUsuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var sucesso = await _usuarioService.CriarUsuarioAsync(novoUsuario);
                if (sucesso)
                    return Ok(novoUsuario);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest($"Mensagem: {e.Message} StackTrace: {e.StackTrace}");
            }
        }

        [HttpPatch("Update")]       
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> EditarUsuario(int id, [FromBody] UsuarioAtualizarDto usuarioaAtualizar)
        {
            try
            {
                var usuario = await _usuarioService.CarregarUsuarioPorIdAsync(id);
                if (usuario != null)
                {
                    var sucesso = await _usuarioService.AlterarUsuarioAsync(int.Parse(SessaoUtils.GetUserId(HttpContext)), usuarioaAtualizar);
                    if (sucesso)
                        return Ok(usuario);
                    else
                        return BadRequest("Falha ao editar usuário.");
                }
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest($"Mensagem: {e.Message} StackTrace: {e.StackTrace}");
            }
        }
        #endregion
    }
}