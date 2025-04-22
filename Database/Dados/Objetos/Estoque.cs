using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Estoque
    {
        #region Propriedades Privadas
        int pID_ESTOQUE { get; set; }
        int pID_EMPRESA { get; set; }
        int pID_RESPONSAVEL { get; set; }
        string pTX_NOME { get; set; }
        bool pIN_ATIVO { get; set; }
        #endregion

        #region Propriedades
        [Key]
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

        [Required]
        [ForeignKey("Empresa")]
        public int ID_EMPRESA
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
        [ForeignKey("Usuario")]
        public int ID_RESPONSAVEL
        {
            get
            {
                return this.pID_RESPONSAVEL;
            }
            set
            {
                this.pID_RESPONSAVEL = value;
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
        #endregion       
    }
}