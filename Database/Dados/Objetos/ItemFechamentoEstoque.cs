using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class ItemFechamentoEstoque
    {
        #region Propriedades Privadas
        long pID_FECHAMENTO { get; set; }
        long pID_ESTOQUE { get; set; }
        DateTime pDT_FECHAMENTO { get; set; }
        bool pIN_ERRO { get; set; }
        #endregion

        #region Propriedades
        public long ID_FECHAMENTO
        {
            get
            {
                return this.pID_FECHAMENTO;
            }
            set
            {
                this.pID_FECHAMENTO = value;
            }
        }

        [Required]
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

        public DateTime DT_FECHAMENTO
        {
            get
            {
                return this.pDT_FECHAMENTO;
            }
            set
            {
                this.pDT_FECHAMENTO = value;
            }
        }

        public bool IN_ERRO
        {
            get
            {
                return this.pIN_ERRO;
            }
            set
            {
                this.pIN_ERRO = value;
            }
        }
        #endregion

        #region Construtores
        public ItemFechamentoEstoque()
        {
            this.ID_FECHAMENTO = -1;
            this.ID_ESTOQUE = -1;
            this.DT_FECHAMENTO = DateTime.MinValue;
            this.IN_ERRO = false;
        }

        public ItemFechamentoEstoque(long idFechamento)
        {
            this.CarregarItemFechamentoEstoque(idFechamento);
        }
        #endregion

        #region Métodos Públicos
        public void CriarItemFechamentoEstoque()
        {
            try
            {
                _ = ctxItemFechamentoEstoque.CreateItemFechamentoEstoque(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CarregarItemFechamentoEstoque(long idFechamento)
        {
            try
            {
                var itemFechamentoEstoque = ctxItemFechamentoEstoque.GetItemFechamentoEstoque(idFechamento).Result;
                this.ID_FECHAMENTO = itemFechamentoEstoque.ID_FECHAMENTO;
                this.ID_ESTOQUE = itemFechamentoEstoque.ID_ESTOQUE;
                this.DT_FECHAMENTO = itemFechamentoEstoque.DT_FECHAMENTO;
                this.IN_ERRO = itemFechamentoEstoque.IN_ERRO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarItemFechamentoEstoque()
        {
            try
            {
                _ = ctxItemFechamentoEstoque.UpdateItemFechamentoEstoque(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesativarItemFechamentoEstoque()
        {
            try
            {
                _ = ctxItemFechamentoEstoque.DeleteItemFechamentoEstoque(this.ID_FECHAMENTO).Result;
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