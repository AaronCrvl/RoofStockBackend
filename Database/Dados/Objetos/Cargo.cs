using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Cargo
    {
        #region Propriedades Privadas
        long pID_CARGO { get; set; }
        string pTX_NOME { get; set; }
        #endregion

        #region Propriedades
        public long ID_CARGO
        {
            get
            {
                return this.pID_CARGO;
            }
            set
            {
                this.pID_CARGO = value;
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
        public Cargo()
        {
            this.ID_CARGO = -1;
            this.TX_NOME = string.Empty;
        }

        public Cargo(long idCargo)
        {
            this.CarregarCargo(idCargo);
        }
        #endregion

        #region Métodos Públicos
        public void CriarCargo()
        {
            try
            {
                _ = ctxCargo.CreateCargo(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CarregarCargo(long idCargo)
        {
            try
            {
                var cargo = ctxCargo.GetCargo(idCargo).Result;
                this.ID_CARGO = cargo.ID_CARGO;
                this.TX_NOME = cargo.TX_NOME;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarCargo()
        {
            try
            {
                _ = ctxCargo.UpdateCargo(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesativarCargo()
        {
            try
            {
                _ = ctxCargo.DeleteCargo(this.ID_CARGO).Result;
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