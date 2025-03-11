using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Estoque
    {
        #region Propriedades Privadas
        long pID_ESTOQUE { get; set; }
        long pID_EMPRESA { get; set; }
        long pID_RESPONSAVEL { get; set; }
        string pNM_ESTOQUE { get; set; }
        bool pIN_ATIVO { get; set; }
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
        public long ID_EMPRESA
        {
            get
            {
                return this.pID_EMPRESA;
            }
            set
            {
                this.pID_EMPRESA = value;
            }
        }

        [Required]
        public long ID_RESPONSAVEL
        {
            get
            {
                return this.pID_RESPONSAVEL;
            }
            set
            {
                this.pID_RESPONSAVEL = value;
            }
        }

        [Required]
        public string NM_ESTOQUE
        {
            get
            {
                return this.pNM_ESTOQUE;
            }
            set
            {
                this.pNM_ESTOQUE = value;
            }
        }

        public bool IN_ATIVO
        {
            get
            {
                return this.pIN_ATIVO;
            }
            set
            {
                this.pIN_ATIVO = value;
            }
        }
        #endregion

        #region Construtores
        public Estoque()
        {
            this.ID_ESTOQUE = -1;
            this.ID_EMPRESA = -1;
            this.ID_RESPONSAVEL = -1;
            this.NM_ESTOQUE = string.Empty;
            this.IN_ATIVO = true;
        }

        public Estoque(long Id)
        {
            this.CarregarEstoque(Id);
        }
        #endregion

        #region Métodos Públicos
        public void CriarEstoque()
        {
            try
            {
                _ = ctxEstoque.CreateEstoque(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CarregarEstoque(long Id)
        {
            try
            {
                var estoque = ctxEstoque.GetEstoque(Id).Result;
                this.ID_ESTOQUE = estoque.ID_ESTOQUE;
                this.ID_EMPRESA = estoque.ID_EMPRESA;
                this.ID_RESPONSAVEL = estoque.ID_RESPONSAVEL;
                this.NM_ESTOQUE = estoque.NM_ESTOQUE;
                this.IN_ATIVO = estoque.IN_ATIVO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarEstoque()
        {
            try
            {
                _ = ctxEstoque.UpdateEstoque(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesativarEstoque()
        {
            try
            {
                _ = ctxEstoque.DeleteEstoque(this.ID_ESTOQUE).Result;
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