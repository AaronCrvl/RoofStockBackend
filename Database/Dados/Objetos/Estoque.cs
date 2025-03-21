using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Estoque
    {
        #region Propriedades Privadas
        long pID_ESTOQUE { get; set; }
        long pID_EMPRESA { get; set; }
        long pID_RESPONSAVEL { get; set; }
        string pNM_ESTOQUE { get; set; }
        bool pIN_ATIVO { get; set; }
        #endregion

        #region Propriedades
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
        public long ID_RESPONSAVEL
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
        public string NM_ESTOQUE
        {
            get
            {
                return this.pNM_ESTOQUE;
            }
            set
            {
                this.pNM_ESTOQUE = value;
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