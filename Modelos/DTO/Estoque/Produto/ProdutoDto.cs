namespace RoofStockBackend.Modelos.DTO.Estoque.Produto
{
    public class ProdutoDto
    {
        public int idProduto { get; set; }
        public string nomeProduto { get; set;}        
        public string nomeMarca { get; set; }
        public double valor { get; set; }    
        public bool promocao { get; set; }  
    }
}
