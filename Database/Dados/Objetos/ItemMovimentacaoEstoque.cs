using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RoofStockBackend.Modelos.DTO.Movimentação_Estoque;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class ItemMovimentacaoEstoque
    {
        #region Propriedades Privadas
        int pID_ITEM_MOVIMENTACAO { get; set; }
        int pID_MOVIMENTACAO { get; set; }
        int pID_PRODUTO { get; set; }
        int pQN_MOVIMENTACAO { get; set; }
        int pCORTESIAS { get; set; }
        int pQUEBRAS { get; set; }
        bool pIN_PROCESSADO { get; set; }
        #endregion

        #region Propriedades
        [Key]
        public int ID_ITEM_MOVIMENTACAO
        {
            get
            {
                return this.pID_ITEM_MOVIMENTACAO;
            }
            set
            {
                this.pID_ITEM_MOVIMENTACAO = value;
            }
        }

        [Required]
        public int ID_MOVIMENTACAO
        {
            get
            {
                return this.pID_MOVIMENTACAO;
            }
            set
            {
                this.pID_MOVIMENTACAO = value;
            }
        }

        [Required]
        [ForeignKey("Produto")]
        public int ID_PRODUTO
        {
            get
            {
                return this.pID_PRODUTO;
            }
            set
            {
                this.pID_PRODUTO = value;
            }
        }

        [Required]
        public int QN_MOVIMENTACAO
        {
            get
            {
                return this.pQN_MOVIMENTACAO;
            }
            set
            {
                this.pQN_MOVIMENTACAO = value;
            }
        }

        [Required]
        public int CORTESIAS
        {
            get
            {
                return this.pCORTESIAS;
            }
            set
            {
                this.pCORTESIAS = value;
            }
        }
        [Required]
        public int QUEBRAS
        {
            get
            {
                return this.pQUEBRAS;
            }
            set
            {
                this.pQUEBRAS = value;
            }
        }

        public bool IN_PROCESSADO
        {
            get
            {
                return this.pIN_PROCESSADO;
            }
            set
            {
                this.pIN_PROCESSADO = value;
            }
        }
        #endregion

        #region Construtores
        public ItemMovimentacaoEstoque()
        {
            this.ID_ITEM_MOVIMENTACAO = -1;
            this.ID_MOVIMENTACAO = -1;
            this.ID_PRODUTO = -1;
            this.QN_MOVIMENTACAO = 0;
            this.CORTESIAS = 0;
            this.QUEBRAS = 0;
            this.IN_PROCESSADO = false;
        }
        #endregion

        public static ItemMovimentacaoEstoque ConvertDtoToBDObject(ItemMovimentacaoEstoqueDto item)
        {
            return new ItemMovimentacaoEstoque
            {
                ID_MOVIMENTACAO = item.idMovimentacao,
                ID_PRODUTO = item.idProduto,
                IN_PROCESSADO = item.processado,
                QN_MOVIMENTACAO = item.quantidadeMovimentacao,
                CORTESIAS = item.cortesias,
                QUEBRAS = item.quebras
            };
        }
    }
}
// !_!