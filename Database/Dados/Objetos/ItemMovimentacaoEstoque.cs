using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class ItemMovimentacaoEstoque
    {
        #region Propriedades Privadas
        long pID_ITEM_MOVIMENTACAO { get; set; }
        long pID_MOVIMENTACAO { get; set; }
        long pID_PRODUTO { get; set; }
        int pQN_MOVIMENTACAO { get; set; }
        bool pIN_PROCESSADO { get; set; }
        #endregion

        #region Propriedades
        [Key]
        public long ID_ITEM_MOVIMENTACAO
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
        public long ID_MOVIMENTACAO
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
        public long ID_PRODUTO
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
            this.IN_PROCESSADO = false;
        }       
        #endregion
    }
}
// !_!