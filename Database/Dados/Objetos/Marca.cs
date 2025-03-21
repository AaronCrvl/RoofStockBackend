using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Marca
    {
        #region Propriedades Privadas
        long pID_MARCA { get; set; }
        string pTX_NOME { get; set; }
        #endregion

        #region Propriedades
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
        #endregion

        #region Construtores
        public Marca()
        {
            this.ID_MARCA = -1;
            this.TX_NOME = string.Empty;
        }      
        #endregion
    }
}
// !_!