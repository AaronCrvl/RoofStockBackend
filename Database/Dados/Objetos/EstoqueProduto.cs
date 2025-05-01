using Microsoft.EntityFrameworkCore;
using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    [Keyless]
    public class EstoqueProduto
    {        
        #region Propriedades Privadas
        int pID_ESTOQUE { get; set; }
        int pID_PRODUTO { get; set; }
        int pQN_ESTOQUE { get; set; }
        #endregion

        #region Propriedades
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
        public int QN_ESTOQUE
        {
            get
            {
                return this.pQN_ESTOQUE;
            }
            set
            {
                this.pQN_ESTOQUE = value;
            }
        }
        #endregion

        #region Construtores
        public EstoqueProduto()
        {
            this.ID_ESTOQUE = -1;
            this.ID_PRODUTO = -1;
            this.QN_ESTOQUE = 0;
        }

        #endregion
    }
}
// !_!