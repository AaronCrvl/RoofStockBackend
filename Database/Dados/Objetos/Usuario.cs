using RoofStockBackend.Contextos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoofStockBackend.Database.Dados.Objetos
{
    public class Usuario
    {
        #region Propriedades Privadas
        long pID_USUARIO { get; set; }
        long pID_FUNCIONARIO { get; set; }
        string pTX_LOGIN { get; set; }
        string pTX_SENHA { get; set; }
        bool pIN_ATIVO { get; set; }
        string pTX_EMAIL { get; set; }
        DateTime pDT_CRIACAO { get; set; }
        #endregion

        #region Propriedades
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
        [ForeignKey("Funcionario")]
        public long ID_FUNCIONARIO
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
            this.DT_CRIACAO = new DateTime();
        }
        public Usuario(int Id)
        {
            this.CarregarUsuario(Id);            
        }
        #endregion

        #region Métodos Públicos
        public void CriarUsuario()
        {
            try
            {
                _ = ctxUsuario.CreateUser(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CarregarUsuario(long Id)
        {
            try
            {
                var user = ctxUsuario.GetUser(Id).Result;
                this.ID_USUARIO = user.ID_USUARIO;
                this.ID_FUNCIONARIO = user.ID_FUNCIONARIO;
                this.TX_LOGIN = user.TX_LOGIN;
                this.pTX_SENHA = user.TX_SENHA;
                this.pTX_EMAIL = user.TX_EMAIL;
                this.IN_ATIVO = user.IN_ATIVO;
                this.DT_CRIACAO = user.DT_CRIACAO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CarregarUsuario(string Username)
        {
            try
            {
                var user = ctxUsuario.GetUser(Username).Result;
                this.ID_USUARIO = user.ID_USUARIO;
                this.ID_FUNCIONARIO = user.ID_FUNCIONARIO;
                this.TX_LOGIN = user.TX_LOGIN;
                this.pTX_SENHA = user.TX_SENHA;
                this.pTX_EMAIL = user.TX_EMAIL;
                this.IN_ATIVO = user.IN_ATIVO;
                this.DT_CRIACAO = user.DT_CRIACAO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void AlterarUsuario()
        {
            try
            {
                _ = ctxUsuario.AlterUser(this).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void DesativarUsuario()
        {
            try
            {
                _ = ctxUsuario.DeleteUser(this.ID_USUARIO).Result;
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