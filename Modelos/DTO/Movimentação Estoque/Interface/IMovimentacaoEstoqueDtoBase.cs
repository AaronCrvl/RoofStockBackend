namespace RoofStockBackend.Modelos.DTO.Movimentação_Estoque.Interface
{
    public interface IMovimentacaoEstoqueDtoBase
    {
        long idMovimentacao { get; set; }
        long idEstoque { get; set; }
        long? idUsuario { get; set; }
        DateTime dataMovimentacao { get; set; }
        long? tipoMovimentacao { get; set; }
        bool processado { get; set; }
    }
}
