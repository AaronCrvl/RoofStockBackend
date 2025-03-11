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

        public Produto(long Id)
        {
            this.CarregarProduto(Id);
        }
        #endregion

        #region Métodos Públicos
        public void CriarProduto()
        {
            try
            {
                _ = ctxProduto.CreateProduto(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CarregarProduto(long Id)
        {
            try
            {
                var produto = ctxProduto.GetProduto(Id).Result;
                this.ID_PRODUTO = produto.ID_PRODUTO;
                this.ID_MARCA = produto.ID_MARCA;
                this.TX_NOME = produto.TX_NOME;
                this.TX_DESCRICAO = produto.TX_DESCRICAO;
                this.VALOR = produto.VALOR;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarProduto()
        {
            try
            {
                _ = ctxProduto.UpdateProduto(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesativarProduto()
        {
            try
            {
                _ = ctxProduto.DeleteProduto(this.ID_PRODUTO).Result;
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