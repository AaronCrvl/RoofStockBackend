namespace RoofStockBackend.Modelos.DTO.Movimentação_Estoque
{
    public class ItemMovimentacaoEstoqueDto
    {
        public int idMovimentacao { get; set; }
        public int idItemMovimentacao { get; set; }
        public int idProduto { get; set; }
        public string nomeProduto { get; set; }
        public int quantidadeMovimentacao { get; set; }
        public bool processado { get; set; }
        public int cortesias { get; set; }
        public int quebras { get; set; }
    }
}
