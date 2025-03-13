using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Models;
using System.Net.Mime;

namespace RoofStockBackend.Controllers
{
    public class cntllEstoque : Controller
    {
        Repositorio.repoUsuario repo;

        public cntllEstoque()
        {
            this.repo = new Repositorio.repoUsuario();
        }

        [HttpGet("GetStock/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStock(long id)
        {
            try
            {
                var selectedUSer = this.repo.CarregarUsuario(id);
                if (selectedUSer.Id > 0)
                    return Ok(selectedUSer);
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("DeleteStock/{username}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStock(string username)
        {
            try
            {
                var selectedUSer = this.repo.CarregarUsuario(username);
                if (selectedUSer.Id > 0)
                    return Ok(selectedUSer);
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("AlterStock")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterUser([FromBody] Usuario user)
        {
            try
            {
                var selectedUSer = this.repo.AlterarUsuario(user);
                if (selectedUSer.Id > 0)
                    return Ok(selectedUSer);
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
// !_!