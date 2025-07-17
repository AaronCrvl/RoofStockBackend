using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Modelos.DTO.Login;
using RoofStockBackend.Services;
using RoofStockBackend.Serviços;
using System.Net.Mime;

namespace RoofStockBackend.Controladores
{
    [ApiController]
    [Tags("Auth")]
    [Route("Auth")]
    public class CntrAutenticacao : ControllerBase
    {
        SrvcAutenticacao _srvc;
        public CntrAutenticacao(SrvcAutenticacao autenticaService)
        {
            _srvc = autenticaService;
        }

        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _srvc.AutenticarDadosToken(loginDto);

                if (string.IsNullOrEmpty(token))
                    return Unauthorized();

                return Ok(new { token, admin = false });
            }
            catch (Exception e)
            {
                return BadRequest($"Mensagem: {e.Message} StackTrace: {e.StackTrace}");
            }
        }       
    }
}
