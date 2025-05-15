namespace RoofStockBackend.Modelos.DTO.Estoque
{
    public class EstoqueAtualizarDto
    {
        public string nomeEstoque { get; set; }
        public int idResponsavel { get; set; }
        public bool ativo { get; set; }
        public OverviewDiario overviewDiario { get; set; }
        public OverviewMensal overviewMensal { get; set; }
    } 
}
