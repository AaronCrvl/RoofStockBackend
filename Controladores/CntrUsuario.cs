using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Services;
using System;
using System.Threading.Tasks;
using System.Net.Mime;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Tags("Usuario")]
    [Route("Usuario")]
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

        [HttpGet("ObterUsuario/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterUsuario(long id)
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

        [HttpGet("ObterUsuario/{username}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPost("CriarUsuario")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuario novoUsuario)
        {
            try
            {
                var novoId = 1;
                novoUsuario.ID_USUARIO = novoId;
                novoUsuario.DT_CRIACAO = DateTime.Now;

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

        [HttpPost("EditarUsuario/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditarUsuario(long id, [FromBody] Usuario usuarioAtualizado)
        {
            try
            {
                var usuario = await _usuarioService.CarregarUsuarioPorIdAsync(id);

                if (usuario != null)
                {
                    usuario.TX_LOGIN = usuarioAtualizado.TX_LOGIN;
                    usuario.TX_SENHA = usuarioAtualizado.TX_SENHA;
                    usuario.TX_EMAIL = usuarioAtualizado.TX_EMAIL;
                    usuario.IN_ATIVO = usuarioAtualizado.IN_ATIVO;

                    var sucesso = await _usuarioService.AlterarUsuarioAsync(usuario);

                    if (sucesso)
                        return Ok(usuario);
                    else
                        return BadRequest("Falha ao editar usuário.");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Mensagem: {e.Message} StackTrace: {e.StackTrace}");
            }
        }

        #endregion
    }
}