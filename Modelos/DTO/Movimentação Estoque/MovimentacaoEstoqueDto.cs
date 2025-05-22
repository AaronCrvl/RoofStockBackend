namespace RoofStockBackend.Modelos.DTO.Movimentação_Estoque
{
    public class MovimentacaoEstoqueDto
    {
        public long idMovimentacao { get; set; }
        public long idEstoque { get; set; }
        public long idUsuario { get; set; }
        public DateTime dataMovimentacao { get; set; }
        public long tipoMovimentacao { get; set; }
        public bool processado { get; set; }
        public IEnumerable<ItemMovimentacaoEstoqueDto> itens { get; set; }
    }
}
