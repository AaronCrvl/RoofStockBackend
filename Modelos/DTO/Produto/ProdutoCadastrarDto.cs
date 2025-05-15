using RoofStockBackend.Modelos.DTO.Produto.Interface;

namespace RoofStockBackend.Modelos.DTO.Produto
{
    public class ProdutoCadastrarDto : IProdutoDtoBase
    {
        public int idProduto { get; set; }
        public string nomeProduto { get; set; }
        public int idMarca { get; set; }
        public int idEstoque { get; set; }
        public double valor { get; set; }
        public bool promocao { get; set; }
        public int quantidade { get; set; }
    }
}
