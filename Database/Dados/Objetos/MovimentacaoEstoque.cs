using RoofStockBackend.Contextos;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RoofStockBackend.Modelos.DTO.Movimentação;
using RoofStockBackend.Modelos.DTO.Movimentação_Estoque.Interface;
using RoofStockBackend.Database.Dados.Enums;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class MovimentacaoEstoque
    {
        #region Propriedades Privadas
        long pID_MOVIMENTACAO { get; set; }
        long pID_ESTOQUE { get; set; }
        long pID_USUARIO { get; set; }
        DateTime pDT_MOVIMENTACAO { get; set; }
        long pTIPO_MOVIMENTACAO { get; set; }
        bool pIN_PROCESSADO { get; set; }
        #endregion

        #region Propriedades
        [Key]
        public long ID_MOVIMENTACAO
        {
            get
            {
                return this.pID_MOVIMENTACAO;
            }
            set
            {
                this.pID_MOVIMENTACAO = value;
            }
        }

        [Required]
        [ForeignKey("Estoque")]
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
        [ForeignKey("Usuario")]
        public long ID_USUARIO
        {
            get
            {
                return this.pID_USUARIO;
            }
            set
            {
                this.pID_USUARIO = value;
            }
        }

        [Required]
        public DateTime DT_MOVIMENTACAO
        {
            get
            {
                return this.pDT_MOVIMENTACAO;
            }
            set
            {
                this.pDT_MOVIMENTACAO = value;
            }
        }

        [Required]
        public long TIPO_MOVIMENTACAO
        {
            get
            {
                return this.pTIPO_MOVIMENTACAO;
            }
            set
            {
                this.pTIPO_MOVIMENTACAO = value;
            }
        }

        public bool IN_PROCESSADO
        {
            get
            {
                return this.pIN_PROCESSADO;
            }
            set
            {
                this.pIN_PROCESSADO = value;
            }
        }
        #endregion

        #region Construtores
        public MovimentacaoEstoque()
        {
            this.ID_MOVIMENTACAO = -1;
            this.ID_ESTOQUE = -1;
            this.ID_USUARIO = -1;
            this.DT_MOVIMENTACAO = DateTime.Now;
            this.TIPO_MOVIMENTACAO = (long)eTipoMovimentacao.Entrada;
            this.IN_PROCESSADO = false;
        }
        #endregion

        public static MovimentacaoEstoque ConvertDtoToBDObject(IMovimentacaoEstoqueDtoBase movimentacao)
        {
            return new MovimentacaoEstoque
            {
                ID_USUARIO = movimentacao.idUsuario == null ? -1 : (long)movimentacao.idUsuario,
                DT_MOVIMENTACAO = movimentacao.dataMovimentacao,
                ID_ESTOQUE = movimentacao.idEstoque,
                IN_PROCESSADO = movimentacao.processado,
                TIPO_MOVIMENTACAO = movimentacao.tipoMovimentacao == null ? -1 : (long)movimentacao.tipoMovimentacao
            };
        }
    }
}
// !_!