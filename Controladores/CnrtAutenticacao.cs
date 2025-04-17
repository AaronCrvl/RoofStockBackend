using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Modelos;
using RoofStockBackend.Services;
using RoofStockBackend.Serviços;
using System.Net.Mime;

namespace RoofStockBackend.Controladores
{
    [ApiController]
    [Tags("Autenticacao")]
    [Route("Autenticacao")]
    public class CnrtAutenticacao : ControllerBase
    {
        SrvcAutenticacao _srvc;
        public CnrtAutenticacao(SrvcAutenticacao autenticaService)
        {
            _srvc = autenticaService;
        }

        [HttpPost("Autenticar")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        
        public async Task<IActionResult> Autenticar([FromBody] LoginUsuarioDto loginDto)
        {
            try
            {
                var token = await _srvc.AutenticarDadosToken(loginDto);

                if (string.IsNullOrEmpty(token))
                    return Unauthorized();

                return Ok(new { token });
            }
            catch (Exception e)
            {
                return BadRequest($"Mensagem: {e.Message} StackTrace: {e.StackTrace}");
            }
        }
    }
}
