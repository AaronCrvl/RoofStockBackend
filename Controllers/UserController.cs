using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RoofStockBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        Repository.UserRepository repo;
        public UserController()
        {
            this.repo = new Repository.UserRepository();
        }

        [HttpGet("Getser/{id}")]
        public Task<ActionResult> GetUser(long id)
        {
            try
            {
                var user = this.repo.GetUser(id);
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    ContentType = Azure.Core.ContentType.ApplicationJson.ToString(),
                    Content = System.Text.Json.JsonSerializer.Serialize(user),
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
