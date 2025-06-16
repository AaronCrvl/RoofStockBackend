using RoofStockBackend.Modelos.DTO.Movimentação_Estoque.Interface;

namespace RoofStockBackend.Modelos.DTO.Fechamento_Estoque
{
    public class FechamentoEstoqueAtualizarDto : IFechamentoEstoqueDtoBase
    {        
        public DateTime dataFechamento { get; set; }
        public DateTime dataInicioPeriodo { get; set; }
        public DateTime dataFinalPeriodo { get; set; }
        public bool erro { get; set; }
        public IEnumerable<ItemFechamentoEstoqueDto> itens { get; set; }
        public int? idFechamentoEstoque { get; set; }
        public int? idEstoque { get; set; }
    }
}
