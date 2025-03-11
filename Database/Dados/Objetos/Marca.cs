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

        public Marca(long idMarca)
        {
            this.CarregarMarca(idMarca);
        }
        #endregion

        #region Métodos Públicos
        public void CriarMarca()
        {
            try
            {
                _ = ctxMarca.CreateMarca(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CarregarMarca(long idMarca)
        {
            try
            {
                var marca = ctxMarca.GetMarca(idMarca).Result;
                this.ID_MARCA = marca.ID_MARCA;
                this.TX_NOME = marca.TX_NOME;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarMarca()
        {
            try
            {
                _ = ctxMarca.UpdateMarca(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesativarMarca()
        {
            try
            {
                _ = ctxMarca.DeleteMarca(this.ID_MARCA).Result;
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