using RoofStockBackend.Modelos.DTO.Estoque.Produto.Interface;

namespace RoofStockBackend.Modelos.DTO.Estoque.Produto
{
    public class ProdutoAtualizarDto : IProdutoDtoBase
    {
        public int idProduto { get; set; }
        public string nomeProduto { get; set; }
        public int idMarca { get; set; }
        public double valor { get; set; }
        public bool promocao { get; set; }
    }
}
