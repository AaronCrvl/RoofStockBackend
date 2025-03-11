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

        public Fornecedor(long Id)
        {
            this.CarregarFornecedor(Id);
        }
        #endregion

        #region Métodos Públicos
        public void CriarFornecedor()
        {
            try
            {
                _ = ctxFornecedor.CreateFornecedor(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CarregarFornecedor(long Id)
        {
            try
            {
                var fornecedor = ctxFornecedor.GetFornecedor(Id).Result;
                this.ID_FORNECEDOR = fornecedor.ID_FORNECEDOR;
                this.TX_NOME = fornecedor.TX_NOME;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarFornecedor()
        {
            try
            {
                _ = ctxFornecedor.UpdateFornecedor(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesativarFornecedor()
        {
            try
            {
                _ = ctxFornecedor.DeleteFornecedor(this.ID_FORNECEDOR).Result;
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