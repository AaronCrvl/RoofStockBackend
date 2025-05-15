using RoofStockBackend.Modelos.DTO.Produto.Interface;

namespace RoofStockBackend.Modelos.DTO.Produto
{
    public class ProdutoAtualizarDto : IProdutoDtoBase
    {
        public int idProduto { get; set; }
        public int idEstoque { get; set; }
        public string nomeProduto { get; set; }
        public int idMarca { get; set; }
        public double valor { get; set; }
        public bool promocao { get; set; }
    }
}
