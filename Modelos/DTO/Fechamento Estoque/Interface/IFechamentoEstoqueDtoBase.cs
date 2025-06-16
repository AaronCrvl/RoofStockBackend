using RoofStockBackend.Modelos.DTO.Fechamento_Estoque;

namespace RoofStockBackend.Modelos.DTO.Movimentação_Estoque.Interface
{
    public interface IFechamentoEstoqueDtoBase
    {
       int? idFechamentoEstoque { get; set; }
       int? idEstoque { get; set; }
       DateTime dataFechamento { get; set; }
       DateTime dataInicioPeriodo { get; set; }
       DateTime dataFinalPeriodo { get; set; }
       bool erro { get; set; }       
    }
}
