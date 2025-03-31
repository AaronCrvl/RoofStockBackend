using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Fornecedor
    {
        #region Propriedades Privadas
        long pID_FORNECEDOR { get; set; }
        string pTX_NOME { get; set; }
        #endregion

        #region Propriedades
        [Key]
        public long ID_FORNECEDOR
        {
            get
            {
                return this.pID_FORNECEDOR;
            }
            set
            {
                this.pID_FORNECEDOR = value;
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
        #endregion

        #region Construtores
        public Fornecedor()
        {
            this.ID_FORNECEDOR = -1;
            this.TX_NOME = string.Empty;
        }      
        #endregion      
    }
}
// !_!