namespace RoofStockBackend.Modelos.DTO.Fechamento_Estoque
{
    public class ItemFechamentoEstoqueDto
    {
        public int idProduto { get; set; }
        public int idFechamentoEstoque { get; set; }        
        public string nomeProduto { get; set; }
        public int quantidadeFinal { get; set; }
        public bool divergencia { get; set; }
        public int quantidadeDivergencia { get; set; }
        public int quebras { get; set; }
        public int cortesias { get; set; }
    }
}
