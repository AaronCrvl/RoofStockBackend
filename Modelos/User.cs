using RoofStockBackend.Contexts;

namespace RoofStockBackend.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public long IdFuncionario { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}