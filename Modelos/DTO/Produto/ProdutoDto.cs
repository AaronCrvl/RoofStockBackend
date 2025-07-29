namespace RoofStockBackend.Modelos.DTO.Produto
{
    public class ProdutoDto
    {
        public int idProduto { get; set; }
        public int idEstoque { get; set; }
        public string nomeProduto { get; set; }
        public string nomeMarca { get; set; }
        public double valor { get; set; }
        public bool promocao { get; set; }
        public DateTime dataValidade { get; set; }
        public string nomeResponsavel { get; set; }        
    }
}
