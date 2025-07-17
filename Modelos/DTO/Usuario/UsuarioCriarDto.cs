using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Modelos.DTO.Usuario
{
    public class UsuarioCriarDto
    {        
        public string cpf { get; set; }
        public string cnpjEmpresa { get; set; }
        public string telefone { get; set; }        
        public int cargo { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public bool ativo { get; set; }
        public bool admin { get; set; }
        public string email { get; set; }                            
        public string nomePessoa { get; set; }                
    }
}