namespace RoofStockBackend.Modelos.DTO.Estoque.Produto.Interface
{
    public interface IProdutoDtoBase
    {
        string nomeProduto { get; }
        int idMarca { get; }
        double valor { get; }
        bool promocao { get; }
    }
}
