using RoofStockBackend.Modelos.DTO.Estoque.Produto.Interface;

namespace RoofStockBackend.Modelos.DTO.Estoque.Produto
{
    public class ProdutoCadastrarDto : IProdutoDtoBase
    {
        public string nomeProduto { get; set; }
        public int idMarca{ get; set; }
        public double valor { get; set; }
        public bool promocao { get; set; }
    }
}
