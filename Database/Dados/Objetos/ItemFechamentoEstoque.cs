using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class ItemFechamentoEstoque
    {
        #region Propriedades Privadas
        int pID_FECHAMENTO { get; set; }        
        int pID_PRODUTO { get; set; }
        int pQN_FINAL { get; set; }
        int pQN_QUEBRAS { get; set; }
        int pQN_CORTESIAS { get; set; }
        bool pIN_DIVERGENCIA { get; set; }
        int pQN_DIVERGENCIA { get; set; }
        #endregion

        #region Propriedades
        [ForeignKey("FechamentoEstoque")]
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
        public int QN_FINAL
        {
            get
            {
                return this.pQN_FINAL;
            }
            set
            {
                this.pQN_FINAL = value;
            }
        }
        public int QN_QUEBRAS
        {
            get
            {
                return this.pQN_QUEBRAS;
            }
            set
            {
                this.pQN_QUEBRAS = value;
            }
        }
        public int QN_CORTESIAS
        {
            get
            {
                return this.pQN_CORTESIAS;
            }
            set
            {
                this.pQN_CORTESIAS = value;
            }
        }
        public int QN_DIVERGENCIA
        {
            get
            {
                return this.pQN_DIVERGENCIA;
            }
            set
            {
                this.pQN_DIVERGENCIA = value;
            }
        }
        public bool IN_DIVERGENCIA
        {
            get
            {
                return this.pIN_DIVERGENCIA;
            }
            set
            {
                this.pIN_DIVERGENCIA = value;
            }
        }
        #endregion

        #region Construtores
        public ItemFechamentoEstoque()
        {
            this.ID_FECHAMENTO = -1;            
            this.ID_PRODUTO = -1;            
            this.IN_DIVERGENCIA = false;
            this.QN_CORTESIAS = -1;
            this.QN_DIVERGENCIA = -1;
            this.QN_FINAL = -1;
            this.QN_QUEBRAS = -1;

        }
        #endregion
    }
}
