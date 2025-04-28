namespace RoofStockBackend.Modelos.DTO.Usuario
{
    public class UsuarioCriarDto
    {
        public int id { get; set; }
        public int idFuncionario { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public bool ativo { get; set; }
        public bool admin { get; set; }
        public string email { get; set; }
    }
}