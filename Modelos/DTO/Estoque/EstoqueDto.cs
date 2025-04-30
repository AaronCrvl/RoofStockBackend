using System.Security.Cryptography;

namespace RoofStockBackend.Modelos.DTO.Estoque
{
    public class EstoqueDto
    {
        public int idEstoque { get; set; }
        public string nomeEstoque { get; set; }
        public string nomeResponsavel { get; set; }
        public bool ativo { get; set; }
        public OverviewDiario overviewDiario { get; set; }
        public OverviewMensal overviewMensal { get; set; }
    }

    public class OverviewDiario
    {
        public int produtosEmEstoque { get; set; }
        public int entradasHoje { get; set; }
        public int promocoesAtivas { get; set; }
        public int vencimentosProximos { get; set; }
    }

    public class OverviewMensal
    {        
        public int entradasMes { get; set; }
        public int saidasMes { get; set; }        
    }
}