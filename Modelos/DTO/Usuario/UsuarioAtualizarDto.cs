namespace RoofStockBackend.Modelos.DTO.Usuario
{
    public class UsuarioAtualizarDto
    {
        public string login { get; set; }
        public string senha { get; set; }
        public bool ativo { get; set; }
        public bool admin { get; set; }
        public string email { get; set; }
    }
}