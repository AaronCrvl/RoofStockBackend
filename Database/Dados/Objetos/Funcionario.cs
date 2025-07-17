using RoofStockBackend.Contextos;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Funcionario
    {
        #region Propriedades Privadas        
        int pID_FUNCIONARIO { get; set; }
        int pID_EMPRESA { get; set; }
        int pID_CARGO { get; set; }
        string pTX_NOME { get; set; }
        string pTX_CPF { get; set; }
        string pTX_EMAIL { get; set; }
        string pTX_TELEFONE { get; set; }
        DateTime pDT_ENTRADA { get; set; }
        #endregion

        #region Propriedades
        [Key]
        public int ID_FUNCIONARIO
        {
            get
            {
                return this.pID_FUNCIONARIO;
            }
            set
            {
                this.pID_FUNCIONARIO = value;
            }
        }
        [Required]
        [ForeignKey("Empresa")]
        public int ID_EMPRESA
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
        [ForeignKey("Cargo")]
        public int ID_CARGO
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
        public string TX_CPF
        {
            get
            {
                return this.pTX_CPF;
            }
            set
            {
                this.pTX_CPF = value;
            }
        }
        public string TX_EMAIL
        {
            get
            {
                return this.pTX_EMAIL;
            }
            set
            {
                this.pTX_EMAIL = value;
            }
        }
        public string TX_TELEFONE
        {
            get
            {
                return this.pTX_TELEFONE;
            }
            set
            {
                this.pTX_TELEFONE = value;
            }
        }
        public DateTime DT_ENTRADA
        {
            get
            {
                return this.pDT_ENTRADA;
            }
            set
            {
                this.pDT_ENTRADA = value;
            }
        }
        #endregion

        #region Construtor
        public Funcionario()
        {
            this.ID_FUNCIONARIO = -1;
            this.ID_EMPRESA = -1;
            this.ID_CARGO = -1;
            this.TX_EMAIL = string.Empty;
            this.TX_NOME = string.Empty;
            this.TX_TELEFONE = string.Empty;
            this.TX_CPF = string.Empty;
            this.DT_ENTRADA = new DateTime();
        }
       
        #endregion      
    }
}
// !_!