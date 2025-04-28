namespace RoofStockBackend.Modelos.DTO.Estoque
{
    public class EstoqueDto
    {
        public int idEstoque { get; set; }
        public string nomeEstoque { get; set; }
        public string nomeResponsavel { get; set; }
        public bool ativo { get; set; }
    }
}