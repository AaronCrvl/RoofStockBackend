using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class MovimentacaoEstoque
    {
        #region Propriedades Privadas
        long pID_MOVIMENTACAO { get; set; }
        long pID_ESTOQUE { get; set; }
        long pID_USUARIO { get; set; }
        DateTime pDT_MOVIMENTACAO { get; set; }
        bool pIN_ENTRADA { get; set; }
        bool pIN_PROCESSADO { get; set; }
        #endregion

        #region Propriedades
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
        public long ID_ESTOQUE
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

        [Required]
        public long ID_USUARIO
        {
            get
            {
                return this.pID_USUARIO;
            }
            set
            {
                this.pID_USUARIO = value;
            }
        }

        [Required]
        public DateTime DT_MOVIMENTACAO
        {
            get
            {
                return this.pDT_MOVIMENTACAO;
            }
            set
            {
                this.pDT_MOVIMENTACAO = value;
            }
        }

        [Required]
        public bool IN_ENTRADA
        {
            get
            {
                return this.pIN_ENTRADA;
            }
            set
            {
                this.pIN_ENTRADA = value;
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
        public MovimentacaoEstoque()
        {
            this.ID_MOVIMENTACAO = -1;
            this.ID_ESTOQUE = -1;
            this.ID_USUARIO = -1;
            this.DT_MOVIMENTACAO = DateTime.Now;
            this.IN_ENTRADA = false;
            this.IN_PROCESSADO = false;
        }      
        #endregion
    }
}
// !_!