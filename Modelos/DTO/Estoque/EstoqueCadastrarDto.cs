namespace RoofStockBackend.Modelos.DTO.Estoque
{
    public class EstoqueCadastrarDto
    {
        public int idEstoque { get; set; }
        public string nomeEstoque { get; set; }
        public int idResponsavel { get; set; }
        public bool ativo { get; set; }      
    }
}
