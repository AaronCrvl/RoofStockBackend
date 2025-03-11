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
    }
}