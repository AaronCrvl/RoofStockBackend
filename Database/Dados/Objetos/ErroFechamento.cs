using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class ErroFechamento
    {
        #region Propriedades Privadas
        long pID_ERRO { get; set; }
        string pTX_ERRO { get; set; }
        string pTX_DESCRICAO { get; set; }
        #endregion

        #region Propriedades
        public long ID_ERRO
        {
            get
            {
                return this.pID_ERRO;
            }
            set
            {
                this.pID_ERRO = value;
            }
        }

        [Required]
        public string TX_ERRO
        {
            get
            {
                return this.pTX_ERRO;
            }
            set
            {
                this.pTX_ERRO = value;
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
        #endregion

        #region Construtores
        public ErroFechamento()
        {
            this.ID_ERRO = -1;
            this.TX_ERRO = string.Empty;
            this.TX_DESCRICAO = string.Empty;
        }

        public ErroFechamento(long idErro)
        {
            this.CarregarErroFechamento(idErro);
        }
        #endregion

        #region Métodos Públicos
        public void CriarErroFechamento()
        {
            try
            {
                _ = ctxErroFechamento.CreateErroFechamento(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CarregarErroFechamento(long idErro)
        {
            try
            {
                var erroFechamento = ctxErroFechamento.GetErroFechamento(idErro).Result;
                this.ID_ERRO = erroFechamento.ID_ERRO;
                this.TX_ERRO = erroFechamento.TX_ERRO;
                this.TX_DESCRICAO = erroFechamento.TX_DESCRICAO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarErroFechamento()
        {
            try
            {
                _ = ctxErroFechamento.UpdateErroFechamento(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesativarErroFechamento()
        {
            try
            {
                _ = ctxErroFechamento.DeleteErroFechamento(this.ID_ERRO).Result;
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