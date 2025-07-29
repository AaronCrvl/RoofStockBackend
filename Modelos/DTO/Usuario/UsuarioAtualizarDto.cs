namespace RoofStockBackend.Modelos.DTO.Usuario
{
    public class UsuarioAtualizarDto
    {
        public string login { get; set; }
        public string senha { get; set; }
        public string nomePessoa { get; set; }                
        public string email { get; set; }
        public string telefone { get; set; }
    }
}