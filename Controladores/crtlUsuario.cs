using Microsoft.AspNetCore.Mvc;
using RoofStockBackend.Models;
using System.Net;
using System.Net.Mime;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrtlUsuario : Controller
    {
        Repositorio.RepoUsuario repo;
        public CrtlUsuario()
        {
            this.repo = new Repositorio.RepoUsuario();
        }

        #region HTTP Methods
        [HttpGet("GetUser/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(long id)
        {
            try
            {
                var user = this.repo.CarregarUsuario(id);

                if (user.Id > 0)
                    return Ok(user);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest("Message: " + e.Message + "StackTrace: " + e.StackTrace);
            }
        }

        [HttpGet("GetUser/{username}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(string username)
        {
            try
            {
                var user = this.repo.CarregarUsuario(username);

                if (user.Id > 0)
                    return Ok(user);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest("Message: " + e.Message + "StackTrace: " + e.StackTrace);
            }
        }

        [HttpPost("CreateUser")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] User newUser)
        {
            try
            {
                var newId = Utils.DBUtils.GenerateNewUserId();
                var user = this.repo.CriarUsuario(
                    new User
                    {
                        Id = newId,
                        Username = newUser.Username,
                        Password = newUser.Password,
                        CreationDate = DateTime.Now
                    });

                if (this.repo.CarregarUsuario(newId).Id > 0)
                    return Ok(user);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest("Message: " + e.Message + "StackTrace: " + e.StackTrace);
            }
        }

        [HttpPost("EditUser/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditUser(long id)
        {
            try
            {
                var user = this.repo.CarregarUsuario(id);

                if (user.Id > 0)
                    return Ok(user);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest("Message: " + e.Message + "StackTrace: " + e.StackTrace);
            }
        }
        #endregion      
    }
}