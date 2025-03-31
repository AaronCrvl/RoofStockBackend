using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class ErroFechamento
    {
        #region Propriedades Privadas
        long pID_ERRO { get; set; }
        string pTX_ERRO { get; set; }
        string pTX_DESCRICAO { get; set; }
        #endregion

        #region Propriedades
        [Key]
        public long ID_ERRO
        {
            get
            {
                return this.pID_ERRO;
            }
            set
            {
                this.pID_ERRO = value;
            }
        }

        [Required]
        public string TX_ERRO
        {
            get
            {
                return this.pTX_ERRO;
            }
            set
            {
                this.pTX_ERRO = value;
            }
        }

        public string TX_DESCRICAO
        {
            get
            {
                return this.pTX_DESCRICAO;
            }
            set
            {
                this.pTX_DESCRICAO = value;
            }
        }
        #endregion

        #region Construtores
        public ErroFechamento()
        {
            this.ID_ERRO = -1;
            this.TX_ERRO = string.Empty;
            this.TX_DESCRICAO = string.Empty;
        }
        #endregion
    }
}

// !_!