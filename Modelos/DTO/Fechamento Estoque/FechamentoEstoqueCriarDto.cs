using RoofStockBackend.Modelos.DTO.Movimentação_Estoque.Interface;

namespace RoofStockBackend.Modelos.DTO.Fechamento_Estoque
{
    public class FechamentoEstoqueCriarDto : IFechamentoEstoqueDtoBase
    {
        public int? idFechamentoEstoque { get; set; }
        int? IFechamentoEstoqueDtoBase.idEstoque { get; set; }
        public DateTime dataFechamento { get; set; }
        public DateTime dataInicioPeriodo { get; set; }
        public DateTime dataFinalPeriodo { get; set; }
        public IEnumerable<ItemFechamentoEstoqueDto> itens { get; set; }        
        public bool erro { get; set; }        
    }
}
