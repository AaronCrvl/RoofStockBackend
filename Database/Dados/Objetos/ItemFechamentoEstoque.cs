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
        [Key]
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
        #endregion
    }
}
// !_!

/*
 {
        idProduto: 0,
        nomeProduto: "Teste 01",
        quantidadeFinal: 0,
        divergencia: false,
        quantidadeDivergencia: 0,
        quebrasContabilizadas: 0,
        cortesias: 0,
      },
    ],
 */