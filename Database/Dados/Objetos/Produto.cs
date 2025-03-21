using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Produto
    {
        #region Propriedades Privadas
        long pID_PRODUTO { get; set; }
        long pID_MARCA { get; set; }
        string pTX_NOME { get; set; }
        string pTX_DESCRICAO { get; set; }
        float pVALOR { get; set; }
        #endregion

        #region Propriedades
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
        public long ID_MARCA
        {
            get
            {
                return this.pID_MARCA;
            }
            set
            {
                this.pID_MARCA = value;
            }
        }

        [Required]
        public string TX_NOME
        {
            get
            {
                return this.pTX_NOME;
            }
            set
            {
                this.pTX_NOME = value;
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

        [Required]
        public float VALOR
        {
            get
            {
                return this.pVALOR;
            }
            set
            {
                this.pVALOR = value;
            }
        }
        #endregion

        #region Construtores
        public Produto()
        {
            this.ID_PRODUTO = -1;
            this.ID_MARCA = -1;
            this.TX_NOME = string.Empty;
            this.TX_DESCRICAO = string.Empty;
            this.VALOR = 0.0f;
        }      
        #endregion
    }
}
// !_!