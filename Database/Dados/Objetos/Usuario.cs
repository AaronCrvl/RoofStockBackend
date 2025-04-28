using RoofStockBackend.Contextos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Usuario
    {
        #region Propriedades Privadas
        int pID_USUARIO { get; set; }
        int pID_FUNCIONARIO { get; set; }
        string pTX_LOGIN { get; set; }
        string pTX_SENHA { get; set; }
        bool pIN_ATIVO { get; set; }
        bool pIN_ADMIN { get; set; }
        string pTX_EMAIL { get; set; }
        DateTime pDT_CRIACAO { get; set; }
        #endregion

        #region Propriedades
        [Key]
        public int ID_USUARIO
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
        [ForeignKey("Funcionario")]
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
        public string TX_LOGIN
        {
            get
            {
                return this.pTX_LOGIN;
            }
            set
            {
                this.pTX_LOGIN = value;
            }
        }
        public string TX_SENHA
        {
            get
            {
                return this.pTX_SENHA;
            }
            set
            {
                this.pTX_SENHA = value;
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
        public bool IN_ADMIN
        {
            get
            {
                return this.pIN_ADMIN;
            }
            set
            {
                this.pIN_ADMIN = value;
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
        public DateTime DT_CRIACAO
        {
            get
            {
                return this.pDT_CRIACAO;
            }
            set
            {
                this.pDT_CRIACAO = value;
            }
        }
        #endregion

        #region Construtor
        public Usuario()
        {
            this.ID_USUARIO = -1;
            this.ID_FUNCIONARIO = -1;
            this.TX_LOGIN = string.Empty;
            this.TX_SENHA = string.Empty;
            this.TX_EMAIL = string.Empty;
            this.IN_ATIVO = false;
            this.IN_ADMIN = false;
            this.DT_CRIACAO = new DateTime();
        }        
        #endregion

        #region Métodos Públicos
       
        #endregion      
    }
}
// !_!