using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RoofStockBackend.Database.Dados.Enums;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Produto
    {
        #region Propriedades Privadas
        int pID_PRODUTO { get; set; }
        int pID_MARCA { get; set; }
        string pTX_NOME { get; set; }
        string pTX_DESCRICAO { get; set; }
        double pVALOR { get; set; }
        bool pIN_PROMOCAO { get; set; }

        int pTIPO_PRODUTO { get; set; }
        #endregion

        #region Propriedades
        [Key]
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
        [ForeignKey("Marca")]
        public int ID_MARCA
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
        public double VALOR
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

        public bool IN_PROMOCAO
        {
            get
            {
                return this.pIN_PROMOCAO;
            }
            set
            {
                this.pIN_PROMOCAO = value;
            }
        }

        public int TIPO_PRODUTO
        {
            get
            {
                return pTIPO_PRODUTO;
            }
            set
            {
                pTIPO_PRODUTO = value;
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
            this.IN_PROMOCAO = false;
            this.TIPO_PRODUTO = (int)eTipoProduto.NaoAlcolico;
        }      
        #endregion
    }
}
// !_!