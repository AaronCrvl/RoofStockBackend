using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class FechamentoEstoque
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
        public FechamentoEstoque()
        {
            this.ID_FECHAMENTO = -1;
            this.ID_ESTOQUE = -1;
            this.DT_FECHAMENTO = DateTime.MinValue;
            this.IN_ERRO = false;
        }

        public FechamentoEstoque(long idFechamento)
        {
            this.CarregarFechamentoEstoque(idFechamento);
        }
        #endregion

        #region Métodos Públicos
        public void CriarFechamentoEstoque()
        {
            try
            {
                _ = ctxFechamentoEstoque.CreateFechamentoEstoque(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CarregarFechamentoEstoque(long idFechamento)
        {
            try
            {
                var fechamentoEstoque = ctxFechamentoEstoque.GetFechamentoEstoque(idFechamento).Result;
                this.ID_FECHAMENTO = fechamentoEstoque.ID_FECHAMENTO;
                this.ID_ESTOQUE = fechamentoEstoque.ID_ESTOQUE;
                this.DT_FECHAMENTO = fechamentoEstoque.DT_FECHAMENTO;
                this.IN_ERRO = fechamentoEstoque.IN_ERRO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarFechamentoEstoque()
        {
            try
            {
                _ = ctxFechamentoEstoque.UpdateFechamentoEstoque(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesativarFechamentoEstoque()
        {
            try
            {
                _ = ctxFechamentoEstoque.DeleteFechamentoEstoque(this.ID_FECHAMENTO).Result;
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