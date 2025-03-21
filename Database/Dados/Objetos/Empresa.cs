using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Empresa
    {
        #region Propriedades Privadas
        long pID_EMPRESA { get; set; }
        string pTX_RAZAO_SOCIAL { get; set; }
        string pTX_TOKEN { get; set; }
        string pTX_CNPJ { get; set; }
        bool pIN_ATIVO { get; set; }
        string pTX_EMAIL { get; set; }
        DateTime pDT_CRIACAO { get; set; }
        #endregion

        #region Propriedades
        public long ID_EMPRESA
        {
            get
            {
                return this.pID_EMPRESA;
            }
            set
            {
                this.pID_EMPRESA = value;
            }
        }

        [Required]
        public string TX_RAZAO_SOCIAL
        {
            get
            {
                return this.pTX_RAZAO_SOCIAL;
            }
            set
            {
                this.pTX_RAZAO_SOCIAL = value;
            }
        }

        public string TX_TOKEN
        {
            get
            {
                return this.pTX_TOKEN;
            }
            set
            {
                this.pTX_TOKEN = value;
            }
        }

        [Required]
        public string TX_CNPJ
        {
            get
            {
                return this.pTX_CNPJ;
            }
            set
            {
                this.pTX_CNPJ = value;
            }
        }

        public bool IN_ATIVO
        {
            get
            {
                return this.pIN_ATIVO;
            }
            set
            {
                this.pIN_ATIVO = value;
            }
        }

        [Required]
        public string TX_EMAIL
        {
            get
            {
                return this.pTX_EMAIL;
            }
            set
            {
                this.pTX_EMAIL = value;
            }
        }

        public DateTime DT_CRIACAO
        {
            get
            {
                return this.pDT_CRIACAO;
            }
            set
            {
                this.pDT_CRIACAO = value;
            }
        }
        #endregion

        #region Construtores
        public Empresa()
        {
            this.ID_EMPRESA = -1;
            this.TX_RAZAO_SOCIAL = string.Empty;
            this.TX_TOKEN = string.Empty;
            this.TX_CNPJ = string.Empty;
            this.IN_ATIVO = true;
            this.TX_EMAIL = string.Empty;
            this.DT_CRIACAO = DateTime.Now;
        }      
        #endregion
    }
}
// !_!