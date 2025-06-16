using RoofStockBackend.Contextos;
using RoofStockBackend.Modelos.DTO.Fechamento_Estoque;
using RoofStockBackend.Modelos.DTO.Movimentação_Estoque.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class FechamentoEstoque
    {
        #region Propriedades Privadas
        int pID_FECHAMENTO { get; set; }
        int pID_ESTOQUE { get; set; }
        DateTime pDT_FECHAMENTO { get; set; }
        DateTime pDT_INICIO_PERIODO { get; set; }
        DateTime pDT_FINAL_PERIODO { get; set; }
        bool pIN_ERRO { get; set; }
        #endregion

        #region Propriedades
        [Key]
        public int ID_FECHAMENTO
        {
            get
            {
                return this.pID_FECHAMENTO;
            }
            set
            {
                this.pID_FECHAMENTO = value;
            }
        }

        [Required]
        public int ID_ESTOQUE
        {
            get
            {
                return this.pID_ESTOQUE;
            }
            set
            {
                this.pID_ESTOQUE = value;
            }
        }

        public DateTime DT_FECHAMENTO
        {
            get
            {
                return this.pDT_FECHAMENTO;
            }
            set
            {
                this.pDT_FECHAMENTO = value;
            }
        }

        public DateTime DT_INICIO_PERIODO
        {
            get
            {
                return this.pDT_INICIO_PERIODO;
            }
            set
            {
                this.pDT_INICIO_PERIODO = value;
            }
        }

        public DateTime DT_FINAL_PERIODO
        {
            get
            {
                return this.pDT_FINAL_PERIODO;
            }
            set
            {
                this.pDT_FINAL_PERIODO = value;
            }
        }

        public bool IN_ERRO
        {
            get
            {
                return this.pIN_ERRO;
            }
            set
            {
                this.pIN_ERRO = value;
            }
        }
        #endregion

        #region Construtores
        public FechamentoEstoque()
        {
            this.ID_FECHAMENTO = -1;
            this.ID_ESTOQUE = -1;
            this.DT_FECHAMENTO = DateTime.MinValue;
            this.IN_ERRO = false;
        }

        #endregion
        public static FechamentoEstoque ConvertDtoToObj(IFechamentoEstoqueDtoBase fechamentoEstoqueDto)
        {
            return new FechamentoEstoque
            {
                ID_FECHAMENTO = (int)(fechamentoEstoqueDto.idFechamentoEstoque == null ? -1 : fechamentoEstoqueDto.idFechamentoEstoque),
                ID_ESTOQUE = (int)(fechamentoEstoqueDto.idEstoque == null ? -1 : fechamentoEstoqueDto.idEstoque),
                DT_FECHAMENTO = fechamentoEstoqueDto.dataFechamento,
                IN_ERRO = fechamentoEstoqueDto.erro,
                DT_INICIO_PERIODO = fechamentoEstoqueDto.dataInicioPeriodo,
                DT_FINAL_PERIODO = fechamentoEstoqueDto.dataFinalPeriodo,
            };
        }
    }
}
// !_!