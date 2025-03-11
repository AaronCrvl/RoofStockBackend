using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class EstoqueProduto
    {        
        #region Propriedades Privadas
        long pID_ESTOQUE { get; set; }
        long pID_PRODUTO { get; set; }
        int pQN_ESTOQUE { get; set; }
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

        public EstoqueProduto(long idEstoque, long idProduto)
        {
            this.CarregarEstoqueProduto(idEstoque, idProduto);
        }
        #endregion

        #region Métodos Públicos
        public void CriarEstoqueProduto()
        {
            try
            {
                _ = ctxEstoqueProduto.CreateEstoqueProduto(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CarregarEstoqueProduto(long idEstoque, long idProduto)
        {
            try
            {
                var estoqueProduto = ctxEstoqueProduto.GetEstoqueProduto(idEstoque, idProduto).Result;
                this.ID_ESTOQUE = estoqueProduto.ID_ESTOQUE;
                this.ID_PRODUTO = estoqueProduto.ID_PRODUTO;
                this.QN_ESTOQUE = estoqueProduto.QN_ESTOQUE;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarEstoqueProduto()
        {
            try
            {
                _ = ctxEstoqueProduto.UpdateEstoqueProduto(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesativarEstoqueProduto()
        {
            try
            {
                _ = ctxEstoqueProduto.DeleteEstoqueProduto(this.ID_ESTOQUE, this.ID_PRODUTO).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
// !_!