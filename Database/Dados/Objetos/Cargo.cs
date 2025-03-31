using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Cargo
    {
        #region Propriedades Privadas
        long pID_CARGO { get; set; }
        string pTX_NOME { get; set; }
        #endregion

        #region Propriedades
        public long ID_CARGO
        {
            get
            {
                return this.pID_CARGO;
            }
            set
            {
                this.pID_CARGO = value;
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
        public Cargo()
        {
            this.ID_CARGO = -1;
            this.TX_NOME = string.Empty;
        }
        
        #endregion      
    }
}
// !_!