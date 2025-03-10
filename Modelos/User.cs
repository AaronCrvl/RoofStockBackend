using RoofStockBackend.Contexts;

namespace RoofStockBackend.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreationDate { get; set; }

        public void EditUser()
        {
            try
            {
                _ = UsuarioContexto.AlterUser(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CreateUser()
        {
            try
            {
                _ = UsuarioContexto.CreateUser(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeactivateUser()
        {
            try
            {
                _ = UsuarioContexto.AlterUser(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
