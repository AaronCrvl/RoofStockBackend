namespace RoofStockBackend.Modelos.DTO.Usuario
{
    public class UsuarioDto
    {
        public long id { get; set; }
        public long idFuncionario { get; set; }
        public bool ativo { get; set; }
        public bool admin { get; set; }
        public string email { get; set; }
    }
}