namespace RoofStockBackend.Modelos.DTO.Empresa
{
    public class EmpresaDto
    {
        public int id { get; set; }
        public string razaoSocial { get; set; }
        public string cnpj { get; set; }
        public bool ativo { get; set; }
        public string email { get; set; }
    }
}